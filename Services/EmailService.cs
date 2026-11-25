using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using MailKit;

namespace DeptPick.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendOtpEmailAsync(string toEmail, string otpCode)
        {
           
            Console.WriteLine("=========================================");
            Console.WriteLine($"🔴 OTP Code for {toEmail} is: {otpCode}");
            Console.WriteLine("=========================================");
            
            try
            {
                var emailSettings = _config.GetSection("EmailSettings");
                var email = emailSettings["Email"];
                var password = emailSettings["Password"];
                var host = emailSettings["Host"];
                var port = int.Parse(emailSettings["Port"] ?? "587");

                // لو الإيميل والباسورد مش موجودين في الإعدادات، الكود هيتخطى الإرسال
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) return;

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("DeptPick System", email));
                message.To.Add(new MailboxAddress("Student", toEmail));
                message.Subject = "Your OTP Code - DeptPick";
                message.Body = new TextPart("plain")
                {
                    Text = $"Your OTP code is: {otpCode}. It will expire in 5 minutes."
                };

                using var client = new SmtpClient();
                await client.ConnectAsync(host, port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(email, password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                Console.WriteLine("✅ Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to send email: {ex.Message}");
            }
        }
    }
}

