using HousingHubBackend.Models;

namespace HousingHubBackend.Services.Interfaces
{
    public interface IAuthService
    {
        UserAccount Authenticate(string email, string password);
        UserAccount Register(UserAccount user);
    }
}