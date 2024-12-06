using api.Data;
using api.Models;
using api.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class TimeCapsuleRepository : ITimeCapsuleRepository
    {
        private readonly AppDbContext _context;

        public TimeCapsuleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TimeCapsule> CreateTimeCapsuleAsync(int userId, CreateTimeCapsul dto)
        {
            var capsule = new TimeCapsule
            {
                Title = dto.Title,
                Message = dto.Message,
                FileUrl = dto.FileUrl ?? "",
                ScheduledDelivery = dto.ScheduledDelivery,
                UserId = userId
            };

            _context.TimeCapsules.Add(capsule);
            await _context.SaveChangesAsync();
            return capsule;
        }

        public async Task<IEnumerable<TimeCapsule>> GetTimeCapsulesAsync(int userId)
        {
            return await _context.TimeCapsules
                                  .Where(tc => tc.UserId == userId)
                                  .ToListAsync();
        }

        public async Task<TimeCapsule> GetTimeCapsuleAsync(int userId, int id)
        {
            return await _context.TimeCapsules
                                  .FirstOrDefaultAsync(tc => tc.Id == id && tc.UserId == userId);
        }

        public async Task<bool> UpdateTimeCapsuleAsync(int userId, int id, UpdateCapsule updatedCapsule)
        {
            var capsule = await _context.TimeCapsules
                                         .FirstOrDefaultAsync(tc => tc.Id == id && tc.UserId == userId);

            if (capsule == null) return false;

            // Only update fields that are not null
            if (!string.IsNullOrEmpty(updatedCapsule.Title))
                capsule.Title = updatedCapsule.Title;

            if (!string.IsNullOrEmpty(updatedCapsule.Message))
                capsule.Message = updatedCapsule.Message;

            if (!string.IsNullOrEmpty(updatedCapsule.FileUrl))
                capsule.FileUrl = updatedCapsule.FileUrl;

            if (updatedCapsule.ScheduledDelivery != null)
                capsule.ScheduledDelivery = updatedCapsule.ScheduledDelivery.Value;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTimeCapsuleAsync(int userId, int id)
        {
            var capsule = await _context.TimeCapsules
                                         .FirstOrDefaultAsync(tc => tc.Id == id && tc.UserId == userId);

            if (capsule == null) return false;

            _context.TimeCapsules.Remove(capsule);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
