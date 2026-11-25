namespace DeptPick
{
    public class AllocationRound
    {
        public int Id { get; set; }
        public int RegistrationPeriodId { get; set; }
        public int RoundNumber { get; set; }
        public int TotalStudents { get; set; }
        public int CsCapacity { get; set; }
        public int ItCapacity { get; set; }
        public int IsCapacity { get; set; }
        public int Status { get; set; } = 0; // 0=Pending, 1=Approved, 2=Published
        public int CreatedByAdminId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ApprovedAt { get; set; }
        public DateTime? PublishedAt { get; set; }

        // Navigation
        public RegistrationPeriod RegistrationPeriod { get; set; } = null!;
        public AdminUser CreatedByAdmin { get; set; } = null!;
        public ICollection<AllocationResult> AllocationResults { get; set; } = new List<AllocationResult>();

    }
}
