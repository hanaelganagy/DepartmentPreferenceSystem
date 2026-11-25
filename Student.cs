namespace DeptPick
{
    public class Student
    {
        public int Id { get; set; }
        public string UniversityId { get; set; } = string.Empty;
        public string NationalId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string UniversityEmail { get; set; } = string.Empty;
        public decimal CumulativeGpa { get; set; }
        public decimal? LastSemesterGpa { get; set; }
        public int AcademicYear { get; set; }
        public int CollegeId { get; set; }
        public int RegistrationStatus { get; set; } = 0;
        // 0=NotRegistered, 1=Registered, 2=Confirmed, 3=Allocated, 4=Unallocated
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public College College { get; set; } = null!;
        public ICollection<PreferenceSubmission> PreferenceSubmissions { get; set; } = new List<PreferenceSubmission>();
        public ICollection<AllocationResult> AllocationResults { get; set; } = new List<AllocationResult>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<DeviceToken> DeviceTokens { get; set; } = new List<DeviceToken>();
    }
}
