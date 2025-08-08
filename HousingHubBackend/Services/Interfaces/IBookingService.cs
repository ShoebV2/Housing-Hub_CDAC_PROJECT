using System.Collections.Generic;
using HousingHubBackend.Models;

namespace HousingHubBackend.Services.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<Booking> GetAll();
        Booking GetById(int id);
        Booking Add(Booking booking);
        Booking Update(int id, Booking booking);
        bool Delete(int id);
    }
}