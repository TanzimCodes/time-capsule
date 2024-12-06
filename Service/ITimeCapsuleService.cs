using api.Models;
using api.Models.DTO;

namespace api.Service
{
    public interface ITimeCapsuleService
    {
        Task<TimeCapsule> CreateTimeCapsuleAsync(int userId, CreateTimeCapsul dto);
        Task<IEnumerable<ReturnAllCapsules>> GetTimeCapsulesAsync(int userId);
        Task<TimeCapsule> GetTimeCapsuleAsync(int userId, int id);
        Task<bool> UpdateTimeCapsuleAsync(int userId, int id, UpdateCapsule updatedCapsule);
        Task<bool> DeleteTimeCapsuleAsync(int userId, int id);
    }
}