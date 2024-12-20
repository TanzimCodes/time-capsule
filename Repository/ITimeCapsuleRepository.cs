using api.Models;
using api.Models.DTO;

namespace api.Repository
{
    public interface ITimeCapsuleRepository
    {
        Task<TimeCapsule> CreateTimeCapsuleAsync(int userId, CreateTimeCapsul dto);
        Task<IEnumerable<TimeCapsule>> GetTimeCapsulesAsync(int userId);
        Task<TimeCapsule> GetTimeCapsuleAsync(int userId, int id);
        Task<bool> UpdateTimeCapsuleAsync(int userId, int id, UpdateCapsule updatedCapsule);
        Task<bool> DeleteTimeCapsuleAsync(int userId, int id);
    }
}