// DTOs for Flat
namespace HousingHubBackend.Dtos
{
    public class FlatDto
    {
        public int FlatId { get; set; }
        public int? WingId { get; set; }
        public string FlatNumber { get; set; } = string.Empty;
        public string FloorNumber { get; set; } = string.Empty;
        public double? Area { get; set; }
        public string Status { get; set; } = string.Empty;
        public WingDto Wing { get; set; } // Add this for frontend filtering
    }

    public class CreateFlatDto
    {
        public int? WingId { get; set; }
        public string FlatNumber { get; set; } = string.Empty;
        public string FloorNumber { get; set; } = string.Empty;
        public double? Area { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class UpdateFlatDto
    {
        public int? WingId { get; set; }
        public string FlatNumber { get; set; } = string.Empty;
        public string FloorNumber { get; set; } = string.Empty;
        public double? Area { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}