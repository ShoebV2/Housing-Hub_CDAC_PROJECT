using HousingHubBackend.Data;
using HousingHubBackend.Models;

namespace HousingHubBackend.Repositories
{
    public class AmenityRepository : Repository<Amenity>, IAmenityRepository { public AmenityRepository(HousingHubDBContext ctx) : base(ctx) { } }
    public class BookingRepository : Repository<Booking>, IBookingRepository { public BookingRepository(HousingHubDBContext ctx) : base(ctx) { } }
    public class ComplaintRepository : Repository<Complaint>, IComplaintRepository { public ComplaintRepository(HousingHubDBContext ctx) : base(ctx) { } }
    public class MaintenanceBillRepository : Repository<MaintenanceBill>, IMaintenanceBillRepository { public MaintenanceBillRepository(HousingHubDBContext ctx) : base(ctx) { } }
    public class MaintenanceFeeRepository : Repository<MaintenanceFee>, IMaintenanceFeeRepository { public MaintenanceFeeRepository(HousingHubDBContext ctx) : base(ctx) { } }
    public class SocietyRepository : Repository<Society>, ISocietyRepository { public SocietyRepository(HousingHubDBContext ctx) : base(ctx) { } }
    public class UserAccountRepository : Repository<UserAccount>, IUserAccountRepository { public UserAccountRepository(HousingHubDBContext ctx) : base(ctx) { } }
    public class VisitorRepository : Repository<Visitor>, IVisitorRepository { public VisitorRepository(HousingHubDBContext ctx) : base(ctx) { } }
    public class WingRepository : Repository<Wing>, IWingRepository { public WingRepository(HousingHubDBContext ctx) : base(ctx) { } }
    public class AnnouncementRepository : Repository<Announcement>, IAnnouncementRepository { public AnnouncementRepository(HousingHubDBContext ctx) : base(ctx) { } }
}
