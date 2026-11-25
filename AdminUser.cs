namespace DeptPick
{
    public class AdminUser
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Admin";
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // Navigation
        public ICollection<RegistrationPeriod> CreatedRegistrationPeriods { get; set; } = new List<RegistrationPeriod>();
        public ICollection<AllocationRound> CreatedAllocationRounds { get; set; } = new List<AllocationRound>();
        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    }
}

