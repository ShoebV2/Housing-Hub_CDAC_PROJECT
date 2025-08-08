using System;

namespace HousingHubBackend.Dtos
{
    public class VisitorDto
    {
        public int VisitorId { get; set; }
        public int? FlatId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string VisitorType { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string VehicleNo { get; set; } = string.Empty;
        public string Purpose { get; set; } = string.Empty;
        public DateTime? EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public int? RecordedBy { get; set; }
    }

    public class CreateVisitorDto
    {
        public int? FlatId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string VisitorType { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string VehicleNo { get; set; } = string.Empty;
        public string Purpose { get; set; } = string.Empty;
        public DateTime? EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public int? RecordedBy { get; set; }
    }

    public class UpdateVisitorDto
    {
        public int? FlatId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string VisitorType { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string VehicleNo { get; set; } = string.Empty;
        public string Purpose { get; set; } = string.Empty;
        public DateTime? EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public int? RecordedBy { get; set; }
    }
}
