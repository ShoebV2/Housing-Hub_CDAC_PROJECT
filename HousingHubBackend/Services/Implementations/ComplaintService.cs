using System.Collections.Generic;
using System.Linq;
using HousingHubBackend.Models;
using HousingHubBackend.Services.Interfaces;
using HousingHubBackend.Data;

namespace HousingHubBackend.Services.Implementations
{
    public class ComplaintService : IComplaintService
    {
        private readonly HousingHubDBContext _context;
        public ComplaintService(HousingHubDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Complaint> GetAll()
        {
            return _context.Complaints.ToList();
        }

        public Complaint GetById(int id)
        {
            var complaint = _context.Complaints.Find(id);
            if (complaint == null)
                throw new KeyNotFoundException($"Complaint with ID {id} not found.");
            return complaint;
        }

        public Complaint Add(Complaint complaint)
        {
            _context.Complaints.Add(complaint);
            _context.SaveChanges();
            return complaint;
        }

        public Complaint Update(int id, Complaint complaint)
        {
            var existing = _context.Complaints.Find(id);
            if (existing == null)
                throw new KeyNotFoundException($"Complaint with ID {id} not found.");
            existing.FlatId = complaint.FlatId;
            existing.RaisedBy = complaint.RaisedBy;
            existing.Title = complaint.Title;
            existing.Description = complaint.Description;
            existing.Category = complaint.Category;
            existing.Status = complaint.Status;
            existing.CreatedAt = complaint.CreatedAt;
            existing.ResolvedAt = complaint.ResolvedAt;
            _context.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var complaint = _context.Complaints.Find(id);
            if (complaint == null) return false;
            _context.Complaints.Remove(complaint);
            _context.SaveChanges();
            return true;
        }
    }
}