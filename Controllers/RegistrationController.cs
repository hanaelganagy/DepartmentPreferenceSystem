using DeptPick.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeptPick.Controllers
{
    public class RegistrationController :ControllerBase
    {
        private readonly DBCONTEXTcs _context;
        public RegistrationController(DBCONTEXTcs context)
        {
            _context = context;
        }
        [HttpGet("status")]
        public async Task<IActionResult> GetRegistrationStatus()
        {
            var activePeriod = await _context.RegistrationPeriods
                .FirstOrDefaultAsync(r => r.IsActive == true && r.IsClosed == false);

            if (activePeriod == null)
            {
                return Ok(new RegistrationStatusDto
                {
                    IsOpen = false,
                    Message = "Registration has not started yet."
                });
            }
            var now = DateTime.UtcNow;
            if (now < activePeriod.OpensAt)
            {
                return Ok(new RegistrationStatusDto
                {
                    IsOpen = false,
                    Message = "Registration has not started yet.",
                    ClosesAt = activePeriod.ClosesAt,
                    PeriodName = activePeriod.Name
                });
            }
            if (now > activePeriod.ClosesAt)
            {
                return Ok(new RegistrationStatusDto
                {
                    IsOpen = false,
                    Message = "Registration period has closed.",
                    ClosesAt = activePeriod.ClosesAt,
                    PeriodName = activePeriod.Name
                });
            }
            return Ok(new RegistrationStatusDto
            {
                IsOpen = true,
                Message = "Registration is open.",
                ClosesAt = activePeriod.ClosesAt,
                PeriodName = activePeriod.Name
            });
        }

    }
}
