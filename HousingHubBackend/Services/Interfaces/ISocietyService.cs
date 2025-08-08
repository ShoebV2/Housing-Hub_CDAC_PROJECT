using System.Collections.Generic;
using HousingHubBackend.Models;

namespace HousingHubBackend.Services.Interfaces
{
    public interface ISocietyService
    {
        IEnumerable<Society> GetAll();
        Society GetById(int id);
        Society Add(Society society);
        Society Update(int id, Society society);
        bool Delete(int id);
    }
}