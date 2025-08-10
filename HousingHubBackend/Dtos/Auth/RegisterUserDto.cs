// DTO for Register User
namespace HousingHubBackend.Dtos.Auth
{
    public class RegisterUserDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int? SocietyId { get; set; }
        public int? FlatId { get; set; }
    }
}
