using AutoMapper;
using FluentValidation;
using HousingHubBackend.Data;
using HousingHubBackend.Dtos;
using HousingHubBackend.Hubs;
using HousingHubBackend.Models;
using HousingHubBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace HousingHubBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VisitorsController : ControllerBase
    {
        private readonly HousingHubDBContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateVisitorDto> _createValidator;
        private readonly IValidator<UpdateVisitorDto> _updateValidator;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly INotificationService _notificationService;

        public VisitorsController(
            HousingHubDBContext context,
            IMapper mapper,
            IValidator<CreateVisitorDto> createValidator,
            IValidator<UpdateVisitorDto> updateValidator,
            IHubContext<NotificationHub> hubContext,
            INotificationService notificationService)
        {
            _context = context;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _hubContext = hubContext;
            _notificationService = notificationService;
        }

        [HttpGet]
        [Authorize(Roles = "super_admin,admin,security_staff,resident")]
        public IActionResult GetAll()
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var userIdStr = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            int? userId = null;
            if (int.TryParse(userIdStr, out var parsedId)) userId = parsedId;

            var visitors = _context.Visitors.ToList();

            if (userRole == "security_staff" && userId != null)
            {
                // Find security staff's societyId
                var staff = _context.UserAccounts.FirstOrDefault(u => u.UserId == userId);
                if (staff != null && staff.SocietyId != null)
                {
                    // Get all flatIds in the staff's society via Flat -> Wing -> Society
                    var societyWingIds = _context.Wings.Where(w => w.SocietyId == staff.SocietyId).Select(w => w.WingId).ToList();
                    var societyFlatIds = _context.Flats.Where(f => f.WingId != null && societyWingIds.Contains(f.WingId.Value)).Select(f => f.FlatId).ToList();
                    visitors = visitors.Where(v => v.FlatId != null && societyFlatIds.Contains(v.FlatId.Value)).ToList();
                }
                else
                {
                    visitors = new List<Visitor>();
                }
            }
            else if (userRole == "admin" && userId != null)
            {
                // Only show visitors for admin's society using Flat -> Wing -> Society
                var admin = _context.UserAccounts.FirstOrDefault(u => u.UserId == userId);
                if (admin != null && admin.SocietyId != null)
                {
                    var societyWingIds = _context.Wings.Where(w => w.SocietyId == admin.SocietyId).Select(w => w.WingId).ToList();
                    var societyFlatIds = _context.Flats.Where(f => f.WingId != null && societyWingIds.Contains(f.WingId.Value)).Select(f => f.FlatId).ToList();
                    visitors = visitors.Where(v => v.FlatId != null && societyFlatIds.Contains(v.FlatId.Value)).ToList();
                }
                else
                {
                    visitors = new List<Visitor>();
                }
            }
            else if (userRole == "resident" && userId != null)
            {
                // Only show visitors for resident's flat(s)
                var resident = _context.UserAccounts.FirstOrDefault(u => u.UserId == userId);
                var residentFlatIds = new List<int>();
                if (resident != null && resident.FlatId != null)
                {
                    residentFlatIds.Add(resident.FlatId.Value);
                }
                visitors = visitors.Where(v => v.FlatId != null && residentFlatIds.Contains(v.FlatId.Value)).ToList();
            }

            var userIds = visitors.Select(v => v.RecordedBy).Where(id => id != null).Distinct().ToList();
            var users = _context.UserAccounts.Where(u => userIds.Contains(u.UserId)).ToDictionary(u => u.UserId, u => u.Name);
            var dtos = visitors.Select(v => {
                var dto = _mapper.Map<VisitorDto>(v);
                dto.RecordedByName = v.RecordedBy != null && users.ContainsKey(v.RecordedBy.Value) ? users[v.RecordedBy.Value] : null;
                return dto;
            }).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "super_admin,admin,security_staff,resident")]
        public IActionResult Get(int id)
        {
            var visitor = _context.Visitors.Find(id);
            if (visitor == null) return NotFound();
            return Ok(_mapper.Map<VisitorDto>(visitor));
        }

        [HttpPost]
        [Authorize(Roles = "super_admin,admin,security_staff")]
        public IActionResult Create([FromBody] CreateVisitorDto dto)
        {
            var visitor = _mapper.Map<Visitor>(dto);
            _context.Visitors.Add(visitor);
            _context.SaveChanges();

            // Notify resident (if FlatId is available)
            if (visitor.FlatId != null)
            {
                var user = _context.UserAccounts.FirstOrDefault(u => u.FlatId == visitor.FlatId);
                if (user != null)
                {
                    _hubContext.Clients.Group($"flat_{visitor.FlatId}").SendAsync("ReceiveNotification", $"A new visitor has been added for your flat. Visitor: {visitor.Name}");
                    if (!string.IsNullOrEmpty(user.Email))
                    {
                        _notificationService.SendNotification(
                            user.Email,
                            "New Visitor Added",
                            $"A new visitor ({visitor.Name}) has been added for your flat."
                        );
                    }
                }
            }
            return CreatedAtAction(nameof(Get), new { id = visitor.VisitorId }, _mapper.Map<VisitorDto>(visitor));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "super_admin,admin,security_staff")]
        public IActionResult Update(int id, [FromBody] UpdateVisitorDto dto)
        {
            var existing = _context.Visitors.Find(id);
            if (existing == null) return NotFound();
            _mapper.Map(dto, existing);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("exit/{id}")]
        [Authorize(Roles = "super_admin,admin,security_staff")]
        public IActionResult MarkExit(int id)
        {
            var visitor = _context.Visitors.Find(id);
            if (visitor == null) return NotFound();
            if (visitor.ExitTime != null) return BadRequest("Visitor already exited.");
            visitor.ExitTime = DateTime.UtcNow;
            _context.SaveChanges();
            return Ok(_mapper.Map<VisitorDto>(visitor));
        }
    }
}