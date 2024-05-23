using TS.Bootcamp.ECommerce.WebAPI.Models.Abstract;

namespace TS.Bootcamp.ECommerce.WebAPI.Models
{
    public class AppUser : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[64];
        public byte[] PasswordSalt { get; set; } = new byte[128];
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string FullName => string.Join(" ", FirstName, LastName);
    }
}
