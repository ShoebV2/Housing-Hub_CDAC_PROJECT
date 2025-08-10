namespace HousingHubBackend.Dtos.Billing
{
    public class BillingReportDto
    {
        public int FlatId { get; set; }
        public string FlatNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public double MaintenanceCharges { get; set; }
        public double PendingMaintenanceAmount { get; set; }
        public double AmenityCharges { get; set; }
        public double PendingAmenityAmount { get; set; }
        public double TotalAmount { get; set; }
        public double TotalPendingAmount { get; set; }
    }
}
