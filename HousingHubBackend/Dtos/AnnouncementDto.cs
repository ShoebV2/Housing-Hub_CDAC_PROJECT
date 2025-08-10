using System;
using System.Collections.Generic;

namespace HousingHubBackend.Dtos
{
    public class AnnouncementDto
    {
        public int Aid { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? SocietyId { get; set; }
        public bool? IsGlobal { get; set; }
        public List<int> TargetWingIds { get; set; } = new();
        public List<int> TargetFlatIds { get; set; } = new();
    }

    public class CreateAnnouncementDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? SocietyId { get; set; }
        public bool? IsGlobal { get; set; }
        public List<int> TargetWingIds { get; set; } = new();
        public List<int> TargetFlatIds { get; set; } = new();
    }

    public class UpdateAnnouncementDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? SocietyId { get; set; }
        public bool? IsGlobal { get; set; }
        public List<int> TargetWingIds { get; set; } = new();
        public List<int> TargetFlatIds { get; set; } = new();
    }
}
