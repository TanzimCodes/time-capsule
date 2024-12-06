
namespace api.Models

{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public required string Salt { get; set; } // New property for the salt
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
