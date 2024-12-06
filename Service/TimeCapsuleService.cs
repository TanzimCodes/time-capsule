using api.Models;
using api.Models.DTO;
using api.Repository;


namespace api.Service
{
    public class TimeCapsuleService : ITimeCapsuleService
    {
        private readonly ITimeCapsuleRepository _repository;

        public TimeCapsuleService(ITimeCapsuleRepository repository)
        {
            _repository = repository;
        }

        public async Task<TimeCapsule> CreateTimeCapsuleAsync(int userId, CreateTimeCapsul dto)
        {
            // Business logic can be added here if needed (e.g., validation)
            return await _repository.CreateTimeCapsuleAsync(userId, dto);
        }

        public async Task<IEnumerable<ReturnAllCapsules>> GetTimeCapsulesAsync(int userId)
        {
            var now = DateTime.UtcNow;
            var capsules = await _repository.GetTimeCapsulesAsync(userId);

            // Transform data into DTOs with additional business logic
            return capsules.Select(capsule => new ReturnAllCapsules
            {
                Id = capsule.Id,
                Title = capsule.Title,
                ScheduledDelivery = capsule.ScheduledDelivery,
                Message = capsule.ScheduledDelivery <= now ? capsule.Message : null,
                FileUrl = capsule.ScheduledDelivery <= now ? capsule.FileUrl : null,
                Status = capsule.ScheduledDelivery <= now ? "opened" : "locked"
            }).ToList();
        }

        public async Task<TimeCapsule> GetTimeCapsuleAsync(int userId, int id)
        {
            var capsule = await _repository.GetTimeCapsuleAsync(userId, id);

            if (capsule == null || capsule.ScheduledDelivery > DateTime.UtcNow)
            {
                return null; // This can be customized to return a business-specific error message
            }

            return capsule;
        }

        public async Task<bool> UpdateTimeCapsuleAsync(int userId, int id, UpdateCapsule updatedCapsule)
        {
            // Add any business logic here if necessary (e.g., validating updated fields)
            return await _repository.UpdateTimeCapsuleAsync(userId, id, updatedCapsule);
        }

        public async Task<bool> DeleteTimeCapsuleAsync(int userId, int id)
        {
            return await _repository.DeleteTimeCapsuleAsync(userId, id);
        }
    }
}
