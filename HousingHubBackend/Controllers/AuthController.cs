using Microsoft.AspNetCore.Mvc;
using HousingHubBackend.Data;
using HousingHubBackend.Models;
using HousingHubBackend.Dtos.Auth;
using HousingHubBackend.Services.Implementations;
using HousingHubBackend.Services.Interfaces;
using System.Linq;

namespace HousingHubBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly HousingHubDBContext _context;
        private readonly AuthService _authService;
        private readonly JwtTokenService _jwtTokenService;
        private readonly INotificationService _notificationService;

        public AuthController(
            HousingHubDBContext context,
            AuthService authService,
            JwtTokenService jwtTokenService,
            INotificationService notificationService)
        {
            _context = context;
            _authService = authService;
            _jwtTokenService = jwtTokenService;
            _notificationService = notificationService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = _authService.Authenticate(loginDto.Email, loginDto.Password);
            if (user == null)
                return Unauthorized();
            var token = _jwtTokenService.GenerateToken(user);
            // Remove password before returning user object
            var userDto = new {
                user.UserId,
                user.Name,
                user.Email,
                user.Phone,
                user.Role,
                user.SocietyId,
                user.FlatId,
                user.IsVerified
            };
            return Ok(new { token, user = userDto });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto registerDto)
        {
            if (_context.UserAccounts.Any(u => u.Email == registerDto.Email))
                return Conflict("Email already exists");
            var user = new UserAccount
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Phone = registerDto.Phone,
                Password = registerDto.Password,
                Role = registerDto.Role,
                SocietyId = registerDto.SocietyId,
                FlatId = registerDto.FlatId
            };
            var createdUser = _authService.Register(user);
            var token = _jwtTokenService.GenerateToken(createdUser);

            // Find the admin for the user's society
            var admin = _context.UserAccounts.FirstOrDefault(u => u.SocietyId == createdUser.SocietyId && u.Role == "admin");
            string flatNumber = null;
            if (createdUser.FlatId.HasValue)
            {
                var flat = _context.Flats.FirstOrDefault(f => f.FlatId == createdUser.FlatId.Value);
                flatNumber = flat?.FlatNumber;
            }
            if (admin != null && !string.IsNullOrEmpty(admin.Email))
            {
                _notificationService.SendNotification(
                    admin.Email,
                    "New Resident Registered",
                    $"A new resident has registered in your society.<br/><br/>" +
                    $"<b>Name:</b> {createdUser.Name}<br/>" +
                    $"<b>Email:</b> {createdUser.Email}<br/>" +
                    $"<b>Phone:</b> {createdUser.Phone}<br/>" +
                    $"<b>FlatId:</b> {createdUser.FlatId}<br/>" +
                    $"<b>FlatNumber:</b> {flatNumber}<br/>"
                );
            }

            return Ok(new { token });
        }
    }
}