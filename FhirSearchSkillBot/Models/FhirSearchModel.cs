using System.Collections.Generic;
public class FhirSearchModel
{
    public string BaseUrl { get; set; }
    // Patient ID if the search has _subject or Resource is Patient
    public string Patient { get; set; }

    // The FHIR resource that is being searched
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