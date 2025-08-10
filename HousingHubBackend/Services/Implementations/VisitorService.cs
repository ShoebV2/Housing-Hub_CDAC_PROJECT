using System.Collections.Generic;
using System.Linq;
using HousingHubBackend.Models;
using HousingHubBackend.Services.Interfaces;
using HousingHubBackend.Data;

namespace HousingHubBackend.Services.Implementations
{
    public class VisitorService : IVisitorService
    {
        private readonly HousingHubDBContext _context;
        public VisitorService(HousingHubDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Visitor> GetAll()
        {
            return _context.Visitors.ToList();
        }

        public Visitor GetById(int id)
        {
            var visitor = _context.Visitors.Find(id);
            if (visitor == null)
                throw new KeyNotFoundException($"Visitor with ID {id} not found.");
            return visitor;
        }

        public Visitor Add(Visitor visitor)
        {
            _context.Visitors.Add(visitor);
            _context.SaveChanges();
            return visitor;
        }

        public Visitor Update(int id, Visitor visitor)
        {
            var existing = _context.Visitors.Find(id);
            if (existing == null)
                throw new KeyNotFoundException($"Visitor with ID {id} not found.");
            existing.FlatId = visitor.FlatId;
            existing.Name = visitor.Name;
            existing.VisitorType = visitor.VisitorType;
            existing.Phone = visitor.Phone;
            existing.VehicleNo = visitor.VehicleNo;
            existing.Purpose = visitor.Purpose;
            existing.EntryTime = visitor.EntryTime;
            existing.ExitTime = visitor.ExitTime;
            existing.RecordedBy = visitor.RecordedBy;
            _context.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var visitor = _context.Visitors.Find(id);
            if (visitor == null) return false;
            _context.Visitors.Remove(visitor);
            _context.SaveChanges();
            return true;
        }
    }
}