/**

@author: beapen

*/
using System.Collections.Generic;
namespace FhirSearchSkillBot.Models
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
        
        
        private string _fhirSearchString = "";
        //Constructor
        public FhirSearchModel()
        {
            SearchParam = new List<string>();
            SearchQualifier = new List<string>();
            SearchValue = new List<string>();

        }

        /// <summary>
        /// Renders the FHIR search string
        /// </summary>
        public string FhirSearchString {
            get{
                
                    if (SearchResource == null)
                    {
                        _fhirSearchString += "Patient?_id=" + Patient + "&";
                    }
                
                    _fhirSearchString += SearchResource + "?";
                    // Iteratively add all search parameters
                    for (int i = 0; i < SearchParam.Count; i++){
                        _fhirSearchString += SearchParam[i] + SearchQualifier[i] + SearchValue[i] + "&";
                    }
                    if (Patient != null)
                    {
                        _fhirSearchString += "_subject=" + Patient;
                    }
                return BaseUrl + _fhirSearchString;
            }
        }

        

    }
}