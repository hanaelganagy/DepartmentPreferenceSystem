using DeptPick.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeptPick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController:ControllerBase
    {
        private readonly DBCONTEXTcs _context;
        public DepartmentsController(DBCONTEXTcs context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var ActivePeriod = await _context.RegistrationPeriods
                .FirstOrDefaultAsync(e => e.IsActive == true && e.IsClosed == false);

            if (ActivePeriod == null || DateTime.UtcNow < ActivePeriod.OpensAt || DateTime.UtcNow > ActivePeriod.ClosesAt)
            {
                return BadRequest(new { message = "Registration is not open." +
                    " You cannot select departments now." });
            }
            var departments = await _context.Departments
               .Include(d => d.College)
               .Where(d => d.IsActive == true) 
               .Select(d => new DepartmentDto
               {
                   Id = d.Id,
                   Code = d.Code,
                   Name = d.Name,
                   CollegeName = d.College.Name
               })
               .ToListAsync();

            return Ok(departments);

        }
    }
}
