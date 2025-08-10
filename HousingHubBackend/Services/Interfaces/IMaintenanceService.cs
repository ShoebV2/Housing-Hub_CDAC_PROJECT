using System.Collections.Generic;
using HousingHubBackend.Models;

namespace HousingHubBackend.Services.Interfaces
{
    public interface IMaintenanceService
    {
        IEnumerable<MaintenanceBill> GetAllBills();
        MaintenanceBill GetBillById(int id);
        MaintenanceBill AddBill(MaintenanceBill bill);
        MaintenanceBill UpdateBill(int id, MaintenanceBill bill);
        bool DeleteBill(int id);
    }
}