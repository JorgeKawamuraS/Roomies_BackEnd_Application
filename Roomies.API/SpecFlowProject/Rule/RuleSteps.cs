using NUnit.Framework;
using SpecFlowProject4;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowProject.Rule
{
    [Binding]
    public class RuleSteps : BaseTest
    {
        private string RuleEndpoint { get; set; }

        public RuleSteps()
        {
            RuleEndpoint = $"{ApiUri}api/rules";
        }



        [When(@"rules required attributes provided to initialize instances")]
        public void WhenRulesRequiredAttributesProvidedToInitializeInstances(Table dtos)
        {
            foreach (var row in dtos.Rows)
            {
                try
                {
                    var rule = row.CreateInstance<Roomies.API.Publication.Domain.Models.Rule>();
                    var data = JsonData(rule);
                    var result = Task.Run(async () => await Client.PostAsync(RuleEndpoint, data)).Result;
                    Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Save Rule Integration Test Completed");
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(false, ex.Message);
                }
            }
        }
        


        [When(@"the landlord complete the form to update the rule with Id (.*) and click the Update button")]
        public void WhenTheLandlordCompleteTheFormToUpdateTheRuleWithIdAndClickTheUpdateButton(int ruleId, Table dto)
        {
            try
            {
                var rule = dto.CreateInstance<Roomies.API.Publication.Domain.Models.Rule>();
                var data = JsonData(rule);
                var result = Task.Run(async () => await Client.PutAsync($"{RuleEndpoint}/{ruleId}", data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Update Rule Integration Test Completed");
                var ruleToCompare = ObjectData<Roomies.API.Publication.Domain.Models.Rule>(result.Content.ReadAsStringAsync().Result);
                Assert.IsTrue(dto.IsEquivalentToInstance(ruleToCompare));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }

        
        [When(@"the landlord select rule with id (.*)")]
        public void WhenTheLandlordSelectRuleWithId(int ruleId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{RuleEndpoint}/{ruleId}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Rule by Id Integration Test Completed");
        }

        [Then(@"rule details should be")]
        public void ThenRuleDetailsShouldBe(Table dto)
        {
            var rule = dto.CreateInstance<Roomies.API.Publication.Domain.Models.Rule>();
            var result = Task.Run(async () => await Client.GetAsync($"{RuleEndpoint}/{rule.Id}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Rule Details Integration Test Completed");
            var ruleToCompare = ObjectData<Roomies.API.Publication.Domain.Models.Rule>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.IsEquivalentToInstance(ruleToCompare));
        }



        [When(@"the landlord goes to Rules Page, rule list should return")]
        public void WhenTheLandlordGoesToRulesPageRuleListShouldReturn(Table dto)
        {
            var result = Task.Run(async () => await Client.GetAsync(RuleEndpoint)).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get All Rules Integration Test Completed");
            var rules = ObjectData<List<Roomies.API.Publication.Domain.Models.Rule>>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.RowCount == rules.Count, "Input and Out rules count matched");
        }
        


        [When(@"the landlord with id (.*) click the Delete Rule button")]
        public void WhenTheLandlordWithIdClickTheDeleteRuleButton(int ruleId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{RuleEndpoint}/{ruleId}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK);
        }
        
        [Then(@"the landlord with id (.*) is removed and removed rule details should be")]
        public void ThenTheLandlordWithIdIsRemovedAndRemovedRuleDetailsShouldBe(int ruleId, Table dto)
        {
            try
            {
                var result = Task.Run(async () => await Client.DeleteAsync($"{RuleEndpoint}/{ruleId}")).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Delete Rule Integration Test Completed");
                var ruleToCompare = ObjectData<Roomies.API.Publication.Domain.Models.Rule>(result.Content.ReadAsStringAsync().Result);
                Assert.IsTrue(dto.IsEquivalentToInstance(ruleToCompare));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }
    }
}
