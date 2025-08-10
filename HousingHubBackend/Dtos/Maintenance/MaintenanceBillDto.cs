using System;

namespace HousingHubBackend.Dtos.Maintenance
{
    public class MaintenanceBillDto
    {
        public int Mbid { get; set; }
        public int? FlatId { get; set; }
        public int? Mfid { get; set; }
        public DateOnly? PeriodStart { get; set; }
        public DateOnly? PeriodEnd { get; set; }
        public DateOnly? DueDate { get; set; }
        public double? Amount { get; set; }
        public DateOnly? PaidDate { get; set; }
        public bool? Paid { get; set; }
        public string TransactionId { get; set; } = string.Empty;
    }
}
