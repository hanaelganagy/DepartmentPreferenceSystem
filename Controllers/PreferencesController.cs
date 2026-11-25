
using DeptPick.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace DeptPick.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PreferencesController : ControllerBase
    {
        private readonly DBCONTEXTcs _context;

        public PreferencesController(DBCONTEXTcs context)
        {
            _context = context;
        }

        // 1. حفظ المسودة (Save draft)
        [HttpPost("draft")]
        public async Task<IActionResult> SaveDraft([FromBody] SaveDraftDto request)
        {
       
            var studentIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (studentIdClaim == null) return Unauthorized(); 
            int studentId = int.Parse(studentIdClaim);

            
            var activePeriod = await _context.RegistrationPeriods
                .FirstOrDefaultAsync(r => r.IsActive == true && r.IsClosed == false);

            if (activePeriod == null || DateTime.UtcNow < activePeriod.OpensAt || DateTime.UtcNow > activePeriod.ClosesAt)
                return BadRequest(new { message = "Registration is not open." });

            
            if (request.DepartmentIds.Distinct().Count() != request.DepartmentIds.Count)
                return BadRequest(new { message = "Duplicate departments are not allowed." });

            
            var submission = await _context.PreferenceSubmissions
                .Include(p => p.StudentPreferences)
                .FirstOrDefaultAsync(p => p.StudentId == studentId && p.RegistrationPeriodId == activePeriod.Id);

            if (submission == null)
            {
                
                submission = new PreferenceSubmission
                {
                    StudentId = studentId,
                    RegistrationPeriodId = activePeriod.Id,
                    Status = 0, 
                    CreatedAt = DateTime.UtcNow
                };
                _context.PreferenceSubmissions.Add(submission);
            }
            else
            {
                _context.StudentPreferences.RemoveRange(submission.StudentPreferences);
            }
            for (int i = 0; i < request.DepartmentIds.Count; i++)
            {
                submission.StudentPreferences.Add(new StudentPreference
                {
                    DepartmentId = request.DepartmentIds[i],
                    Priority = i + 1 
                });
            }

            submission.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Draft saved successfully.", submissionId = submission.Id });
        }

        // 2. عرض المسودة (Get draft) - عشان لو قفل ورجع
        [HttpGet("draft")]
        public async Task<IActionResult> GetDraft()
        {
            
            var studentIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (studentIdClaim == null) return Unauthorized(); 
            int studentId = int.Parse(studentIdClaim);

            var submission = await _context.PreferenceSubmissions
                .Include(p => p.StudentPreferences)
                    .ThenInclude(sp => sp.Department)
                .Where(p => p.StudentId == studentId && p.Status == 0) 
                .OrderByDescending(p => p.UpdatedAt)
                .FirstOrDefaultAsync();

            if (submission == null)
                return NotFound(new { message = "No draft found." });

            var response = new DraftResponseDto
            {
                SubmissionId = submission.Id,
                Status = "Draft",
                UpdatedAt = submission.UpdatedAt,
                Preferences = submission.StudentPreferences
                    .OrderBy(sp => sp.Priority)
                    .Select(sp => new PreferenceItemDto
                    {
                        DepartmentId = sp.DepartmentId,
                        DepartmentName = sp.Department.Name,
                        Priority = sp.Priority
                    }).ToList()
            };

            return Ok(response);
        }
        
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmPreferences()
        {
           
            var studentIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (studentIdClaim == null) return Unauthorized(); 
            int studentId = int.Parse(studentIdClaim);

            
            var activePeriod = await _context.RegistrationPeriods
                .FirstOrDefaultAsync(r => r.IsActive == true && r.IsClosed == false);

            if (activePeriod == null || DateTime.UtcNow < activePeriod.OpensAt || DateTime.UtcNow > activePeriod.ClosesAt)
                return BadRequest(new { message = "Registration is not open. Cannot confirm." });

           
            var submission = await _context.PreferenceSubmissions
                .Include(p => p.StudentPreferences)
                .FirstOrDefaultAsync(p => p.StudentId == studentId && p.RegistrationPeriodId == activePeriod.Id);

            if (submission == null)
                return BadRequest(new { message = "No draft found. Please select your preferences first." });

            
            if (submission.StudentPreferences.Count == 0)
                return BadRequest(new { message = "You must select at least one department." });

            
            if (submission.Status == 1)
                return BadRequest(new { message = "You have already confirmed your preferences." });

            
            submission.Status = 1; 
            submission.SubmittedAt = DateTime.UtcNow;
            submission.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // بنرجع رقم الطلب (Application reference) زي اللي في الشاشة
            var applicationReference = $"DP-{submission.Id.ToString("D5")}";

            return Ok(new
            {
                message = "Preferences confirmed successfully.",
                applicationReference = applicationReference,
                submittedAt = submission.SubmittedAt
            });
        }
        [HttpGet("me")]
        public async Task<IActionResult> GetMyPreferences()
        {
            
            var studentIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (studentIdClaim == null) return Unauthorized();
            int studentId = int.Parse(studentIdClaim);

            var submission = await _context.PreferenceSubmissions
                .Include(p => p.StudentPreferences)
                    .ThenInclude(sp => sp.Department)
                .Include(p => p.RegistrationPeriod)
                .Where(p => p.StudentId == studentId && p.Status == 1) 
                .OrderByDescending(p => p.SubmittedAt)
                .FirstOrDefaultAsync();

            if (submission == null)
                return NotFound(new { message = "No confirmed application found." });

            // حساب وقت إغلاق التعديل (مثلاً 6 ساعات من وقت التأكيد)
            var editDeadline = submission.SubmittedAt?.AddHours(6);

            var response = new
            {
                ApplicationReference = $"DP-{submission.Id.ToString("D5")}",
                SubmittedAt = submission.SubmittedAt,
                EditDeadline = editDeadline, // للعداد التنازلي
                RegistrationClosesAt = submission.RegistrationPeriod.ClosesAt,
                Preferences = submission.StudentPreferences
                    .OrderBy(sp => sp.Priority)
                    .Select(sp => new
                    {
                        Priority = sp.Priority,
                        DepartmentName = sp.Department.Name,
                        DepartmentId = sp.DepartmentId
                    }).ToList()
            };

            return Ok(response);
        }
        [HttpPut("me/edit")]
        public async Task<IActionResult> EditPreferences([FromBody] SaveDraftDto request)
        {
            
            var studentIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (studentIdClaim == null) return Unauthorized(); 
            int studentId = int.Parse(studentIdClaim);

            var submission = await _context.PreferenceSubmissions
                .Include(p => p.StudentPreferences)
                .Include(p => p.RegistrationPeriod)
                .FirstOrDefaultAsync(p => p.StudentId == studentId && p.Status == 1);

            if (submission == null)
                return NotFound(new { message = "No confirmed application found to edit." });

            // 1. نتأكد إنه لسه في فترة الـ 6 ساعات المسموح بيها
            var editDeadline = submission.SubmittedAt?.AddHours(6);
            if (DateTime.UtcNow > editDeadline)
                return BadRequest(new { message = "Edit window has closed. You can no longer modify your preferences." });

            
            if (submission.RegistrationPeriod.IsClosed || DateTime.UtcNow > submission.RegistrationPeriod.ClosesAt)
                return BadRequest(new { message = "Registration period has closed." });

            
            if (request.DepartmentIds.Distinct().Count() != request.DepartmentIds.Count)
                return BadRequest(new { message = "Duplicate departments are not allowed." });

            // 4. نمسح الرغبات القديمة ونحط الجديدة
            _context.StudentPreferences.RemoveRange(submission.StudentPreferences);

            for (int i = 0; i < request.DepartmentIds.Count; i++)
            {
                submission.StudentPreferences.Add(new StudentPreference
                {
                    DepartmentId = request.DepartmentIds[i],
                    Priority = i + 1
                });
            }

            submission.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Preferences updated successfully." });
        }
    }
}