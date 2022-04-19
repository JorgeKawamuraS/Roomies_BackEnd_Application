using NUnit.Framework;
using SpecFlowProject4;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowProject.User
{
    [Binding]
    public class UserSteps : BaseTest
    {
        private string UserEndpoint { get; set; }
        private string ProfileEndpoint { get; set; }
        public UserSteps()
        {
            UserEndpoint = $"{ApiUri}api/profile/1/users";
            ProfileEndpoint = $"{ApiUri}api/profiles";
        }

        [When(@"users required attributes provided to initialize instances")]
        public void WhenUsersRequiredAttributesProvidedToInitializeInstances(Table dtos)
        {
            var jsonAdmin = Task.Run(async () => await Client.GetAsync($"{ProfileEndpoint}/1")).Result;

            if (jsonAdmin == null || jsonAdmin.StatusCode != HttpStatusCode.OK)
            {
                try
                {
                    var profile = new Roomies.API.Domain.Models.Profile { 
                        Id = 1, 
                        Name = "Cristiano", 
                        LastName = "Ronaldo", 
                        CellPhone = "984567245",
                        IdCard = "95673718",
                        Description = "Perfil1",
                        Birthday = Convert.ToDateTime(1999-05-21),
                        Department = "Lima",
                        Province = "Lima",
                        District = "Miraflores",
                        Address = "Av. La Molina 3195, Urb. Sol de la Molina"
                    };
                    var JsonAdmin = JsonData(profile);
                    Task.Run(async () => await Client.PostAsync(ProfileEndpoint, JsonAdmin));
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(false, ex.Message);
                }
            }

            //Creating some users
            foreach (var row in dtos.Rows)
            {
                try
                {
                    var user = row.CreateInstance<Roomies.API.Domain.Models.User>();
                    var data = JsonData(user);
                    var result = Task.Run(async () => await Client.PostAsync(UserEndpoint, data)).Result;
                    Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Save User Integration Test Completed");
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(false, ex.Message);
                }
            }
        }


        [When(@"the user complete the form with the required fields and click the Register button")]
        public void WhenTheUserCompleteTheFormWithTheRequiredFieldsAndClickTheRegisterButton(Table dto)
        {
            try
            {
                var user = dto.CreateInstance<Roomies.API.Domain.Models.User>();
                var data = JsonData(user);
                var result = Task.Run(async () => await Client.PostAsync(UserEndpoint, data)).Result;
                Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Save User Integration Test Completed");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
            }
        }
        
        [When(@"the administrator goes to Users Page, user list should return")]
        public void WhenTheAdministratorGoesToUsersPageUserListShouldReturn(Table dto)
        {
            var result = Task.Run(async () => await Client.GetAsync(UserEndpoint)).Result;
            Assert.IsTrue(result != null && result.StatusCode == HttpStatusCode.OK, "Get All Users Integration Test Completed");
            var users = ObjectData<List<Roomies.API.Domain.Models.User>>(result.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(dto.RowCount == users.Count, "Input and Out user count matched");
        }
    }
}
