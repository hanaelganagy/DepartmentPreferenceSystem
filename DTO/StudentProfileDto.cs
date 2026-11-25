namespace DeptPick.DTO
{
    public class StudentProfileDto
    {
        public string FullName { get; set; } = string.Empty;
        public string UniversityId { get; set; } = string.Empty;
        public string CollegeName { get; set; } = string.Empty;
        public int AcademicYear { get; set; }
        public decimal CumulativeGpa { get; set; }
    }
}
