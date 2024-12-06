
namespace api.Models.DTO
{
    public class UpdateCapsule
    {
        public int Id { get; set; } // Primary Key
        public string? Title { get; set; }
        public string? Message { get; set; }
        public string? FileUrl { get; set; } // Optional for media storage
        public DateTime? ScheduledDelivery { get; set; }
    }
}