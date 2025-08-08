namespace HousingHubBackend.Services.Interfaces
{
    public interface INotificationService
    {
        void SendNotification(string to, string subject, string message);
    }
}