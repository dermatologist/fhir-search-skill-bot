// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;
using FhirSearchSkillBot.Bots;
using FhirSearchSkillBot.Models;
namespace FhirSearchSkillBot.Dialogs
{
    public class FhirSearchDialog : ComponentDialog
    {
        private readonly IStatePropertyAccessor<FhirSearchModel> _userProfileAccessor;

        public FhirSearchDialog(UserState userState)
            : base(nameof(FhirSearchDialog))
        {
            _userProfileAccessor = userState.CreateProperty<FhirSearchModel>("FhirSearchModel");

            // This array defines how the Waterfall will execute.
            var waterfallSteps = new WaterfallStep[]
            {
                WantPatientStepAsync,
                GetIdStepAsync,
                ResourceStepAsync,
                ParameterStepAsync,
                OperatorStepAsync,
                ValueStepAsync,
                SummaryStepAsync,
            };

            // Add named dialogs to the DialogSet. These names are saved in the dialog state.
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new TextPrompt(nameof(TextPrompt)));
            AddDialog(new TextPrompt(nameof(TextPrompt)));

            // The initial child Dialog to run.
            InitialDialogId = nameof(WaterfallDialog);
        }


        private static async Task<DialogTurnResult> WantPatientStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // WaterfallStep always finishes with the end of the Waterfall or with another dialog; here it is a Prompt Dialog.
            // Running a prompt here means the next WaterfallStep will be run when the user's response is received.
            return await stepContext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Are you looking for a patient record? (Yes/No)"),
                    Choices = ChoiceFactory.ToChoices(new List<string> { "Yes", "No"}),
                }, cancellationToken);
        }

        private static async Task<DialogTurnResult> GetIdStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            if (((FoundChoice)stepContext.Result).Value == "No")
            {
                // Continuing to next step with an empty patient ID
                await stepContext.Context.SendActivityAsync(
                    MessageFactory.Text("Ok, continuing without a patient ID"),
                    cancellationToken);
                return await stepContext.NextAsync("no", cancellationToken);
            }
            else
            {
                // Otherwise, ask for the patient ID.
                return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("Please enter the patient ID.") }, cancellationToken);
            }

        }

        private static async Task<DialogTurnResult> ResourceStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["patientId"] = (string)stepContext.Result;
            return await stepContext.PromptAsync(nameof(ChoicePrompt),
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("What resource are you looking for?"),
                    Choices = ChoiceFactory.ToChoices(new List<string> { "Patient", "Encounter", "Observation" }),
                }, cancellationToken);
        }

        private static async Task<DialogTurnResult> ParameterStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["resource"] = ((FoundChoice)stepContext.Result).Value;

            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("Please enter the param to search.") }, cancellationToken);
        }
        private static async Task<DialogTurnResult> OperatorStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["param"] = (string)stepContext.Result;

            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("Please enter the operator to search.") }, cancellationToken);
        }
        private static async Task<DialogTurnResult> ValueStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["operator"] = (string)stepContext.Result;

            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("Please enter the value to search.") }, cancellationToken);
        }

        private async Task<DialogTurnResult> SummaryStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            stepContext.Values["value"] = (string)stepContext.Result;

            // Get the current profile object from user state.
            var fhirSearchModel = await _userProfileAccessor.GetAsync(stepContext.Context, () => new FhirSearchModel(), cancellationToken);

            if((string)stepContext.Values["patientId"] != "no") fhirSearchModel.Patient = (string)stepContext.Values["patientId"];
            if((string)stepContext.Values["resource"] != "no") fhirSearchModel.SearchResource = (string)stepContext.Values["resource"];
            if((string)stepContext.Values["param"] != "no") fhirSearchModel.SearchParam.Add((string)stepContext.Values["param"]);
            if((string)stepContext.Values["operator"] != "no") fhirSearchModel.SearchOperator.Add((string)stepContext.Values["operator"]);
            if((string)stepContext.Values["value"] != "no") fhirSearchModel.SearchValue.Add((string)stepContext.Values["value"]);


            var msg = $"I am going to search {fhirSearchModel.FhirSearchString}";

            msg += ".";

            await stepContext.Context.SendActivityAsync(MessageFactory.Text(msg), cancellationToken);


            // WaterfallStep always finishes with the end of the Waterfall or with another dialog; here it is the end.
            return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
        }

    
    }
}