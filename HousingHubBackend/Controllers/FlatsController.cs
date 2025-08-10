using AutoMapper;
using FluentValidation;
using HousingHubBackend.Data;
using HousingHubBackend.Dtos;
using HousingHubBackend.Models;
using HousingHubBackend.Repositories;
using HousingHubBackend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingHubBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FlatsController : ControllerBase
    {
        private readonly IFlatRepository _flatRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateFlatDto> _createValidator;
        private readonly IValidator<UpdateFlatDto> _updateValidator;
        private readonly INotificationService _notificationService;
        private readonly HousingHubDBContext _context;

        public FlatsController(
            IFlatRepository flatRepository,
            IMapper mapper,
            IValidator<CreateFlatDto> createValidator,
            IValidator<UpdateFlatDto> updateValidator,
            INotificationService notificationService,
            HousingHubDBContext context)
        {
            _flatRepository = flatRepository;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _notificationService = notificationService;
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var flats = await _context.Flats.Include(f => f.Wing).ToListAsync();
            var dtos = _mapper.Map<IEnumerable<FlatDto>>(flats);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var flat = await _flatRepository.GetByIdAsync(id);
            if (flat == null) return NotFound();
            return Ok(_mapper.Map<FlatDto>(flat));
        }

        [HttpPost]
        [Authorize(Roles = "super_admin,admin,security_staff")]
        public async Task<IActionResult> Create([FromBody] CreateFlatDto dto)
        {
            var flat = _mapper.Map<Flat>(dto);
            await _flatRepository.AddAsync(flat);

            // Find the society admin's email for the new flat
            // Flat -> Wing -> Society -> UserAccount (Role == admin)
            var wing = _context.Wings.FirstOrDefault(w => w.WingId == flat.WingId);
            if (wing != null)
            {
                var society = _context.Societies.FirstOrDefault(s => s.SocietyId == wing.SocietyId);
                if (society != null)
                {
                    var admin = _context.UserAccounts.FirstOrDefault(u => u.SocietyId == society.SocietyId && u.Role == "admin");
                    if (admin != null && !string.IsNullOrEmpty(admin.Email))
                    {
                        _notificationService.SendNotification(
                            admin.Email,
                            "Flat Created",
                            $"Hey {admin.Name}, New Flat {flat.FlatNumber} has been created in the {wing.Name} Wing of your society."
                        );
                    }
                }
            }

            return CreatedAtAction(nameof(Get), new { id = flat.FlatId }, _mapper.Map<FlatDto>(flat));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "super_admin,admin,security_staff")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFlatDto dto)
        {
            var flat = await _flatRepository.GetByIdAsync(id);
            if (flat == null) return NotFound();

            _mapper.Map(dto, flat);
            await _flatRepository.UpdateAsync(flat);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "super_admin,admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _flatRepository.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}