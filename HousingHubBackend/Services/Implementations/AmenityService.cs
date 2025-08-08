using System.Collections.Generic;
using System.Linq;
using HousingHubBackend.Models;
using HousingHubBackend.Services.Interfaces;
using HousingHubBackend.Data;

namespace HousingHubBackend.Services.Implementations
{
    public class AmenityService : IAmenityService
    {
        private readonly HousingHubDBContext _context;
        public AmenityService(HousingHubDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Amenity> GetAll()
        {
            return _context.Amenities.ToList();
        }

        public Amenity GetById(int id)
        {
            var amenity = _context.Amenities.Find(id);
            if (amenity == null)
                throw new KeyNotFoundException($"Amenity with ID {id} not found.");
            return amenity;
        }

        public Amenity Add(Amenity amenity)
        {
            _context.Amenities.Add(amenity);
            _context.SaveChanges();
            return amenity;
        }

        public Amenity Update(int id, Amenity amenity)
        {
            var existing = _context.Amenities.Find(id);
            if (existing == null)
                throw new KeyNotFoundException($"Amenity with ID {id} not found.");
            existing.SocietyId = amenity.SocietyId;
            existing.Name = amenity.Name;
            existing.Description = amenity.Description;
            existing.HourlyRate = amenity.HourlyRate;
            existing.MaxCapacity = amenity.MaxCapacity;
            existing.OpeningTime = amenity.OpeningTime;
            existing.ClosingTime = amenity.ClosingTime;
            _context.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var amenity = _context.Amenities.Find(id);
            if (amenity == null) return false;
            _context.Amenities.Remove(amenity);
            _context.SaveChanges();
            return true;
        }
    }
}