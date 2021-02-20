* dotnet add package Microsoft.Bot.Builder.Dialogs


## Tests

* dotnet new xunit -o FhirSearchSkillBot.testing
* dotnet add FhirSearchSkillBot.testing/FhirSearchSkillBot.testing.csproj reference FhirSearchSkillBot/FhirSearchSkillBot.csproj
* dotnet test FhirSearchSkillBot.testing/FhirSearchSkillBot.testing.csproj



