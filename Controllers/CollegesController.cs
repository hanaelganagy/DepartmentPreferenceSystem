using DeptPick.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeptPick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegesController : ControllerBase
    {
        public readonly DBCONTEXTcs _context;

        public CollegesController(DBCONTEXTcs context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetCollege()
        {
            var college = await _context.Colleges.Select(c => new CollegeDto
            {
                Id = c.Id,
                Name = c.Name,
                Code = c.Code,
                Status = c.Status
            }).ToListAsync();
            return Ok(college);
        }

    }
}
