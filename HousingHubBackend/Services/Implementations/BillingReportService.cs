using System;
using System.Collections.Generic;
using System.Linq;
using HousingHubBackend.Data;
using HousingHubBackend.Dtos.Billing;
using HousingHubBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using HousingHubBackend.Models;

namespace HousingHubBackend.Services.Implementations
{
    public class BillingReportService : IBillingReportService
    {
        private readonly HousingHubDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BillingReportService(HousingHubDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<BillingReportDto> GenerateReport(BillingReportFilter filter)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var role = user?.FindFirst(ClaimTypes.Role)?.Value;
            var currentUserId = int.Parse(user?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            IQueryable<Flat> flatsQuery = _context.Flats
                .Include(f => f.Wing)
                .Include(f => f.Wing.Society)
                .Include(f => f.UserAccounts)
                .Where(f => f.Status == "Occupied");

            if (role == "admin")
            {
                var societyIdClaim = user?.FindFirst("SocietyId")?.Value;
                int societyId = 0;
                if (!string.IsNullOrEmpty(societyIdClaim))
                {
                    int.TryParse(societyIdClaim, out societyId);
                }
                flatsQuery = flatsQuery.Where(f => f.Wing.SocietyId == societyId);
            }
            else if (role == "resident")
            {
                var flatIdClaim = user?.FindFirst("FlatId")?.Value;
                if (int.TryParse(flatIdClaim, out var flatId))
                {
                    flatsQuery = flatsQuery.Where(f => f.FlatId == flatId);
                }
            }


            else if (role == "super_admin")
            {
                if (filter.SocietyId.HasValue)
                    flatsQuery = flatsQuery.Where(f => f.Wing.SocietyId == filter.SocietyId.Value);
            }

            if (filter.WingId.HasValue)
                flatsQuery = flatsQuery.Where(f => f.WingId == filter.WingId.Value);

            var flats = flatsQuery.ToList();

            var reports = new List<BillingReportDto>();

            foreach (var flat in flats)
            {
                var userAccount = flat.UserAccounts.FirstOrDefault();
                if (userAccount == null) continue;

                // Maintenance
                var maintenanceBills = _context.MaintenanceBills
                    .Where(mb => mb.FlatId == flat.FlatId)
                    .ToList();

                
                double totalMaintenance = maintenanceBills.Sum(m => m.Amount ?? 0.0);
                double unpaidMaintenance = maintenanceBills
                    .Where(m => m.Paid == false)
                    .Sum(m => m.Amount ?? 0.0);
                

                // Amenities
                var bookings = _context.Bookings
                    .Where(b =>
                        b.FlatId == flat.FlatId &&
                        (b.Status == "Approved" || b.Status == "Completed"))
                    .ToList();

                double totalAmenity = bookings.Sum(b => b.Amount ?? 0.0);
                double unpaidAmenity = bookings
                    .Where(b => b.Paid == false)
                    .Sum(b => b.Amount ?? 0.0);

                reports.Add(new BillingReportDto
                {
                    FlatId = flat.FlatId,
                    FlatNumber = flat.FlatNumber,
                    Name = userAccount.Name,
                    Phone = userAccount.Phone,
                    MaintenanceCharges = totalMaintenance,
                    PendingMaintenanceAmount = unpaidMaintenance,
                    AmenityCharges = totalAmenity,
                    PendingAmenityAmount = unpaidAmenity,
                    TotalAmount = totalMaintenance + totalAmenity,
                    TotalPendingAmount = unpaidMaintenance + unpaidAmenity
                });
            }

            return reports;
        }
    }
}
