using System.Linq;
using HousingHubBackend.Models;
using HousingHubBackend.Services.Interfaces;
using HousingHubBackend.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace HousingHubBackend.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly HousingHubDBContext _context;
        private readonly PasswordHasher<UserAccount> _passwordHasher;
        public AuthService(HousingHubDBContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<UserAccount>();
        }

        public UserAccount Authenticate(string email, string password)
        {
            var user = _context.UserAccounts.FirstOrDefault(u => u.Email == email);
            if (user == null)
                return null;
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            if (result == PasswordVerificationResult.Failed)
                return null;
            return user;
        }

        public UserAccount Register(UserAccount user)
        {
            if (_context.UserAccounts.Any(u => u.Email == user.Email))
                throw new KeyNotFoundException($"UserAccount with email {user.Email} already exists.");
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            _context.UserAccounts.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}