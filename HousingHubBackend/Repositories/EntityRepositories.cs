using HousingHubBackend.Models;

namespace HousingHubBackend.Repositories
{
    public interface IAmenityRepository : IRepository<Amenity> { }
    public interface IBookingRepository : IRepository<Booking> { }
    public interface IComplaintRepository : IRepository<Complaint> { }
    public interface IMaintenanceBillRepository : IRepository<MaintenanceBill> { }
    public interface IMaintenanceFeeRepository : IRepository<MaintenanceFee> { }
    public interface ISocietyRepository : IRepository<Society> { }
    public interface IUserAccountRepository : IRepository<UserAccount> { }
    public interface IVisitorRepository : IRepository<Visitor> { }
    public interface IWingRepository : IRepository<Wing> { }
    public interface IAnnouncementRepository : IRepository<Announcement> { }
}
