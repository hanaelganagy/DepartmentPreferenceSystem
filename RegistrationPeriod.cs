namespace DeptPick
{
    public class RegistrationPeriod
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime OpensAt { get; set; }
        public DateTime ClosesAt { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsClosed { get; set; } = false;
        public int? CreatedByAdminId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public AdminUser? CreatedByAdmin { get; set; }
        public ICollection<PreferenceSubmission> PreferenceSubmissions { get; set; } = new List<PreferenceSubmission>();
        public ICollection<AllocationRound> AllocationRounds { get; set; } = new List<AllocationRound>();

    }
}
