namespace DeptPick
{
    public class PreferenceSubmission
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int RegistrationPeriodId { get; set; }
        public int Status { get; set; } = 0; // 0=Draft, 1=Confirmed
        public DateTime? SubmittedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public Student Student { get; set; } = null!;
        public RegistrationPeriod RegistrationPeriod { get; set; } = null!;
        public ICollection<StudentPreference> StudentPreferences { get; set; } = new List<StudentPreference>();
    }
}
