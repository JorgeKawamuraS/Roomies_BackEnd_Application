using Roomies.API.Domain.Models;
using Roomies.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Services
{
    public interface IProfileService
    {
        Task<IEnumerable<Profile>> ListAsync();
        Task<ProfileResponse> GetByIdAsync(int id);
        Task<IEnumerable<Profile>> ListByPlanIdAsync(int planId);
        Task<ProfileResponse> SaveAsync(Profile user,int planId);
        Task<ProfileResponse> UpdateAsync(int id, Profile user);
        Task<ProfileResponse> DeleteAsync(int id);
    }
}
