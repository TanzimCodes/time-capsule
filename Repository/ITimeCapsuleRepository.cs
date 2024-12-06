using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repository
{
    public interface ITimeCapsuleRepository
    {
        Task<IEnumerable<TimeCapsule>> GetAllAsync();
        Task<TimeCapsule> GetByIdAsync(int id);
        Task AddAsync(TimeCapsule capsule);
        Task UpdateAsync(TimeCapsule capsule);
        Task DeleteAsync(int id);
    }
}