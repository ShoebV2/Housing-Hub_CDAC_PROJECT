// DTO for Complaint
namespace HousingHubBackend.Dtos
{
    public class ComplaintDto
    {
        public int ComplaintId { get; set; }
        public int? FlatId { get; set; }
        public int? RaisedBy { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public string? FlatNumber { get; set; }
        public string? WingName { get; set; }
        public string? RaisedByName { get; set; } // Add resident name
        public string? CreatedAtFormatted { get; set; } // dd/MM/yyyy
        public string? ResolvedAtFormatted { get; set; } // dd/MM/yyyy
    }

    public class CreateComplaintDto
    {
        public int? FlatId { get; set; }
        public int? RaisedBy { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
    }

    public class UpdateComplaintDto
    {
        public int? FlatId { get; set; }
        public int? RaisedBy { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
    }
}
