/**

@author: beapen

*/
using System.Collections.Generic;
namespace Nuchange.HealthBots.FhirSearchSkillBot.Models
{
    /// <summary>
    /// FHIR Search Model Class
    /// </summary>
    public class FhirSearchModel
    {
    public string BaseUrl { get; set; }

    /// <summary>
    /// Patient ID if the search has _subject or Resource is Patient
    /// </summary>
    public string Patient { get; set; }

    /// <summary>
    /// The FHIR resource that is being searched
    /// </summary>
    public string SearchResource { get; set; }

    public List<string> SearchParam { get; set; }

    public List<string> SearchQualifier { get; set; }

    public List<string> SearchValue { get; set; }

    public string FhirSearchString {
        get{
            var _fhirSearchString = "?";
            if(Patient != null){
                _fhirSearchString = "_subject=" + Patient;
                if (SearchResource == null)
                {
                    _fhirSearchString = "Patient/_subject=" + Patient;
                }
            }
            _fhirSearchString += SearchResource + "/";
            // Iteratively add all search parameters
            for (int i = 0; i < SearchParam.Count; i++){
                _fhirSearchString += SearchParam[i] + SearchQualifier[i] + SearchValue[i] + "&";
            }
            return BaseUrl + _fhirSearchString;
        }
    }

    }
}