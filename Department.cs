namespace DeptPick
{
    public class Department
    {
        public int Id { get; set; }
        public int CollegeId { get; set; } // Foreign Key
        public string Code { get; set; } = string.Empty; // CS, IT, IS
        public string Name { get; set; } = string.Empty;
        public int DisplayOrder { get; set; } // CS=1, IT=2, IS=3
        public bool IsActive { get; set; } = true;

        // Navigation Property
        public College College { get; set; } = null!;
        public ICollection<StudentPreference> StudentPreferences { get; set; } = new List<StudentPreference>();
        public ICollection<AllocationResult> AllocationResults { get; set; } = new List<AllocationResult>();
    }
}

