using NUnit.Framework;
using SpecFlowProject4;
using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Roomies.API;
using System.Collections.Generic;

namespace SpecFlowProject.Plan
{
    [Binding]
    public class PlanSteps : BaseTest
    {
        private string PlanEndpoint { get; set; }

        public PlanSteps()
        {
            PlanEndpoint = $"{ApiUri}api/plans";
        }
        [When(@"plans required attributes provided to initialize instances")]
        public void WhenPlansRequiredAttributesProvidedToInitializeInstances(Table dtos)
        {
            //Creating some plans
            foreach (var row in dtos.Rows)
            {
                try
                {
                    var plan = row.CreateInstance<Roomies.API.Domain.Models.Plan>();
                    var data = JsonData(plan);
                    var result = Task.Run(async () => await Client.PostAsync(PlanEndpoint, data)).Result;
                    Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Save Plan Integration Test Completed");
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(false, ex.Message);
                }
            }
        }
        


        [When(@"the administrator goes to see all the plans, administrator list should return")]
        public void WhenTheAdministratorGoesToSeeAllThePlansAdministratorListShouldReturn(Table dto)
        {
            var result = Task.Run(async () => await Client.GetAsync(PlanEndpoint)).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get All Plans Integration Test Completed");
            var plans = ObjectData<List<Roomies.API.Domain.Models.Plan>>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.RowCount == plans.Count, "Input and Out plan count matched");
        }




        [When(@"the user goes to Plan Page and click on plan with id (.*)")]
        public void WhenTheUserGoesToPlanPageAndClickOnPlanWithId(int planId)
        {
            var result = Task.Run(async () => await Client.GetAsync($"{PlanEndpoint}/{planId}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get Plan by Id Integration Test Completed");
        }

        [Then(@"user details should be")]
        public void ThenUserDetailsShouldBe(Table dto)
        {
            var plan = dto.CreateInstance<Roomies.API.Domain.Models.Plan>();
            var result = Task.Run(async () => await Client.GetAsync($"{PlanEndpoint}/{plan.Id}")).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Plan Details Integration Test Completed");
            var planToCompare = ObjectData<Roomies.API.Domain.Models.Plan>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.IsEquivalentToInstance(planToCompare));
        } 
    }
}
