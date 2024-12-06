
using api.Data;
using api.Models;
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

        public async Task<IEnumerable<TimeCapsule>> GetAllAsync()
        {
            return await _context.TimeCapsules.ToListAsync();
        }

        public async Task<TimeCapsule> GetByIdAsync(int id)
        {
            return await _context.TimeCapsules.FindAsync(id);
        }

        public async Task AddAsync(TimeCapsule capsule)
        {
            await _context.TimeCapsules.AddAsync(capsule);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TimeCapsule capsule)
        {
            _context.TimeCapsules.Update(capsule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var capsule = await GetByIdAsync(id);
            if (capsule != null)
            {
                _context.TimeCapsules.Remove(capsule);
                await _context.SaveChangesAsync();
            }
        }
    }
}