namespace DeptPick
{
    public class AuditLog
    {
        public long Id { get; set; }
        public string EntityType { get; set; } = string.Empty;
        public string EntityId { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public int? PerformedByAdminId { get; set; }
        public int? PerformedByStudentId { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public string? IpAddress { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public AdminUser? PerformedByAdmin { get; set; }
        public Student? PerformedByStudent { get; set; }
    }
}
