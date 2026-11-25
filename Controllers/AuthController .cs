
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeptPick.DTO;
using DeptPick.Services;
using System;
using System.Threading.Tasks;
namespace DeptPick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController :ControllerBase
    {
        private readonly DBCONTEXTcs _context;
        private readonly EmailService _email;
        private readonly TokenService _tokenService;
        public AuthController(DBCONTEXTcs context, EmailService email, TokenService tokenService)
        {

            _context = context;
            _email = email;
            _tokenService = tokenService;
        }

        [HttpPost("request-otp")]
        public async Task<IActionResult> RequestOtp([FromBody] RequestOtpDto request)
        {
            if (string.IsNullOrEmpty(request.Email))
                return BadRequest(new { message = "Email is required" });

            // 1. التأكد إن الإيميل موجود في الداتا بيز
            var student = await _context.Students.FirstOrDefaultAsync(s => s.UniversityEmail == request.Email);

            if (student == null)
            {
                return NotFound(new { message = "Email not found in university records." });
            }
            var otpCode = new Random().Next(100000, 999999).ToString();
            var otpEntity = new DeptPick.OtpCode
            {
                UniversityEmail = request.Email,
                CodeHash = otpCode, 
                ExpiresAt = DateTime.UtcNow.AddMinutes(5),
                IsUsed = false,
                CreatedAt = DateTime.UtcNow
            };
            _context.OtpCodes.Add(otpEntity);
            await _context.SaveChangesAsync();
            await _email.SendOtpEmailAsync(request.Email, otpCode);
            return Ok(new { message = "OTP sent successfully to your email." });
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDto request)
        {
            var otpRecord = await _context.OtpCodes
                .FirstOrDefaultAsync(o => o.UniversityEmail == request.Email
                                       && o.CodeHash == request.OtpCode
                                       && o.IsUsed == false);
            if (otpRecord == null)
                return BadRequest(new { message = "Invalid OTP code." });

            if (otpRecord.ExpiresAt < DateTime.UtcNow)
                return BadRequest(new { message = "OTP code has expired. Please request a new one." });
            otpRecord.IsUsed = true;
            await _context.SaveChangesAsync();

           
            var student = await _context.Students
                .Include(s => s.College) 
                .FirstOrDefaultAsync(s => s.UniversityEmail == request.Email);

            if (student == null)
                return NotFound(new { message = "Student not found." });
            
            var token = _tokenService.CreateToken(student.Id, student.UniversityEmail, student.FullName);

            
            return Ok(new
            {
                token = token,
                student = new
                {
                    student.Id,
                    student.FullName,
                    student.UniversityId,
                    student.AcademicYear,
                    student.CumulativeGpa,
                    CollegeName = student.College.Name
                }
            });
        }
    }
    }

