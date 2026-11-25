namespace DeptPick
{
    public class StudentPreference
    {
        public int Id { get; set; }
        public int SubmissionId { get; set; }
        public int DepartmentId { get; set; }
        public int Priority { get; set; } // 1, 2, 3

        // Navigation
        public PreferenceSubmission Submission { get; set; } = null!;
        public Department Department { get; set; } = null!;
    }
}
