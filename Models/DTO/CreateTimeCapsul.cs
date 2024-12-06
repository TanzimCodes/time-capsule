using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.DTO
{
    public class CreateTimeCapsul
    {
        public int Id { get; set; } // Primary Key
        public required string Title { get; set; }
        public required string Message { get; set; }
        public required string FileUrl { get; set; } // Optional for media storage
        public required DateTime ScheduledDelivery { get; set; }
    }
}