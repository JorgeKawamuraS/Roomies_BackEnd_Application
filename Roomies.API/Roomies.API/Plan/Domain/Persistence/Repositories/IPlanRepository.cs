using Roomies.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roomies.API.Domain.Repositories
{
    public interface IPlanRepository
    {
        Task<IEnumerable<Plan>> ListAsync();
        Task AddAsync(Plan plan);
        Task<Plan> FindById(int id);
        void Update(Plan plan);
        void Remove(Plan plan);
    }
}
