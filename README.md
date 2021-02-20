# fhir-search-skill-bot
## FHIR Search skill for Bot framework in c#

[FHIR](https://www.hl7.org/fhir/overview.html) is a standard for exchanging healthcare information electronically. Searching for resources is fundamental to the mechanics of FHIR. Search operations traverse through an existing set of resources filtering by parameters supplied to the search operation.

The [Microsoft Bot Framework SDK](https://docs.microsoft.com/en-us/azure/bot-service/index-bf-sdk?view=azure-bot-service-4.0) allows you to create and develop bots for the Azure Bot Service. Starting with version 4.7 of the Bot Framework SDK, you can extend a bot using another bot (a skill). A skill can be consumed by various other bots, facilitating reuse. This repository is such a FHIR search skill for **mapping conversations to FHIR search.** This can be extended to allow **doctors to TALK to their EMRs**.

This is experimental for now. Pull request, welcome!

[Checkout a similar project using RASA](https://github.com/dermatologist/rasaonfhir)

## Author

* [Bell Eapen](https://nuchange.ca) |  [Contact](https://nuchange.ca/contact) | [![Twitter Follow](https://img.shields.io/twitter/follow/beapen?style=social)](https://twitter.com/beapen)
