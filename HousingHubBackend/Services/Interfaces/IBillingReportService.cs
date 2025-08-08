using System.Collections.Generic;
using HousingHubBackend.Dtos.Billing;

namespace HousingHubBackend.Services.Interfaces
{
    public interface IBillingReportService
    {
        List<BillingReportDto> GenerateReport(BillingReportFilter filter);
    }
}
