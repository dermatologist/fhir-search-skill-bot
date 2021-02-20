using System;
using Xunit;
using FhirSearchSkillBot.Models; 
namespace FhirSearchSkillBot.testing
{
    public class FhirSearchModelTest
    {
        [Fact]
        public void TestModel()
        {
            var _fhirSearchModel = new FhirSearchModel();
            // Default setting in model constructor
            // _fhirSearchModel.BaseUrl = @"http://hapi.fhir.org/baseR4/";
            _fhirSearchModel.Patient = "585457";
            _fhirSearchModel.SearchResource = "Encounter";
            _fhirSearchModel.SearchParam.Add("_date");
            _fhirSearchModel.SearchQualifier.Add("=");
            _fhirSearchModel.SearchValue.Add("12-03-2020");
            Console.Write(_fhirSearchModel.FhirSearchString);
        }

        [Fact]
        public void TestApi()
        {
            var _fhirSearchModel = new FhirSearchModel();
            // _fhirSearchModel.BaseUrl = @"http://hapi.fhir.org/baseR4/";
            _fhirSearchModel.Patient = "585457";
            _fhirSearchModel.SearchResource = "Encounter";
            _fhirSearchModel.SearchParam.Add("_date");
            _fhirSearchModel.SearchQualifier.Add("=");
            _fhirSearchModel.SearchValue.Add("12-03-2020");
            Console.Write(_fhirSearchModel.FhirSearch);
        }
    }
}