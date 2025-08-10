namespace HousingHubBackend.Dtos
{
    public class AmenityDto
    {
        public int AmenityId { get; set; }
        public int? SocietyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double? HourlyRate { get; set; }
        public int? MaxCapacity { get; set; }
        public TimeOnly? OpeningTime { get; set; }
        public TimeOnly? ClosingTime { get; set; }
    }

    public class CreateAmenityDto
    {
        public int? SocietyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double? HourlyRate { get; set; }
        public int? MaxCapacity { get; set; }
        public TimeOnly? OpeningTime { get; set; }
        public TimeOnly? ClosingTime { get; set; }
    }

    public class UpdateAmenityDto
    {
        public int? SocietyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double? HourlyRate { get; set; }
        public int? MaxCapacity { get; set; }
        public TimeOnly? OpeningTime { get; set; }
        public TimeOnly? ClosingTime { get; set; }
    }
}
