using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HousingHubBackend.Services.Interfaces;
using HousingHubBackend.Dtos.Billing;
using System.Security.Claims;

namespace HousingHubBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BillingReportController : ControllerBase
    {
        private readonly IBillingReportService _billingReportService;

        public BillingReportController(IBillingReportService billingReportService)
        {
            _billingReportService = billingReportService;
        }

        [HttpPost("generate")]
        [Authorize(Roles = "super_admin,admin,resident")]
        public IActionResult GenerateReport([FromBody] BillingReportFilter filter)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var userSocietyIdClaim = User.FindFirstValue("SocietyId");
            var userFlatIdClaim = User.FindFirstValue("FlatId");

            if (role == "admin")
            {
                if (int.TryParse(userSocietyIdClaim, out var adminSocietyId))
                    filter.SocietyId = adminSocietyId;
                else
                    return BadRequest("Invalid or missing SocietyId claim for admin.");
            }


            if (role == "resident")
            {
                if (!int.TryParse(userFlatIdClaim, out var flatId))
                    return Forbid();

                // Clear filters not needed for residents
                filter.WingId = null;
                filter.SocietyId = null;

                // Let the service filter by flatId using the JWT claim
                var report = _billingReportService.GenerateReport(filter);

                return Ok(report); // Only 1 report will be returned from service
            }



            var fullReport = _billingReportService.GenerateReport(filter);
            return Ok(fullReport);
        }
    }
}
