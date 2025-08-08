using System.Collections.Generic;
using System.Linq;
using HousingHubBackend.Models;
using HousingHubBackend.Services.Interfaces;
using HousingHubBackend.Data;

namespace HousingHubBackend.Services.Implementations
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly HousingHubDBContext _context;
        public MaintenanceService(HousingHubDBContext context)
        {
            _context = context;
        }

        public IEnumerable<MaintenanceBill> GetAllBills()
        {
            return _context.MaintenanceBills.ToList();
        }

        public MaintenanceBill GetBillById(int id)
        {
            var bill = _context.MaintenanceBills.Find(id);
            if (bill == null)
                throw new KeyNotFoundException($"MaintenanceBill with ID {id} not found.");
            return bill;
        }

        public MaintenanceBill AddBill(MaintenanceBill bill)
        {
            _context.MaintenanceBills.Add(bill);
            _context.SaveChanges();
            return bill;
        }

        public MaintenanceBill UpdateBill(int id, MaintenanceBill bill)
        {
            var existing = _context.MaintenanceBills.Find(id);
            if (existing == null)
                throw new KeyNotFoundException($"MaintenanceBill with ID {id} not found.");
            existing.FlatId = bill.FlatId;
            existing.Mfid = bill.Mfid;
            existing.Amount = bill.Amount;
            existing.DueDate = bill.DueDate;
            existing.Paid = bill.Paid;
            existing.PaidDate = bill.PaidDate;
            _context.SaveChanges();
            return existing;
        }

        public bool DeleteBill(int id)
        {
            var bill = _context.MaintenanceBills.Find(id);
            if (bill == null) return false;
            _context.MaintenanceBills.Remove(bill);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<MaintenanceFee> GetAllFees()
        {
            return _context.MaintenanceFees.ToList();
        }

        public MaintenanceFee GetFeeById(int id)
        {
            var fee = _context.MaintenanceFees.Find(id);
            if (fee == null)
                throw new KeyNotFoundException($"MaintenanceFee with ID {id} not found.");
            return fee;
        }

        public MaintenanceFee AddFee(MaintenanceFee fee)
        {
            _context.MaintenanceFees.Add(fee);
            _context.SaveChanges();
            return fee;
        }

        public MaintenanceFee UpdateFee(int id, MaintenanceFee fee)
        {
            var existing = _context.MaintenanceFees.Find(id);
            if (existing == null)
                throw new KeyNotFoundException($"MaintenanceFee with ID {id} not found.");
            existing.WingId = fee.WingId;
            existing.RatePerSqft = fee.RatePerSqft;
            existing.EffectiveFrom = fee.EffectiveFrom;
            existing.EffectiveTo = fee.EffectiveTo;
            existing.IsActive = fee.IsActive;
            existing.CreatedBy = fee.CreatedBy;
            _context.SaveChanges();
            return existing;
        }

        public bool DeleteFee(int id)
        {
            var fee = _context.MaintenanceFees.Find(id);
            if (fee == null) return false;
            _context.MaintenanceFees.Remove(fee);
            _context.SaveChanges();
            return true;
        }
    }
}