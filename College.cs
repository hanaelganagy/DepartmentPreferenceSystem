namespace DeptPick
{
    public class College
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Status { get; set; } // 0=NotConfigured, 1=available
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Property (عشان نعرف نجيب الأقسام اللي جواها)
        public ICollection<Department> Departments { get; set; } = new List<Department>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
