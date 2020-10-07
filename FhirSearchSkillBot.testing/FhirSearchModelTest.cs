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
            _fhirSearchModel.Patient = "23";
            _fhirSearchModel.SearchResource = "Encounter";
            _fhirSearchModel.SearchParam.Add("_date");
            _fhirSearchModel.SearchQualifier.Add("=");
            _fhirSearchModel.SearchValue.Add("12-03-2020");
            Console.Write(_fhirSearchModel.FhirSearchString);
        }
    }
}