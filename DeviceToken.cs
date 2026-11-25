namespace DeptPick
{
    public class DeviceToken
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastUsedAt { get; set; }

        // Navigation
        public Student Student { get; set; } = null!;
    }
}
