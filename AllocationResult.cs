namespace DeptPick
{
    public class AllocationResult
    {
        public int Id { get; set; }
        public int AllocationRoundId { get; set; }
        public int StudentId { get; set; }
        public int? DepartmentId { get; set; } // Nullable لو الطالب Unallocated
        public int? AcceptedPreferencePriority { get; set; }
        public decimal GpaSnapshot { get; set; }
        public bool IsUnallocated { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // Navigation
        public AllocationRound AllocationRound { get; set; } = null!;
        public Student Student { get; set; } = null!;
        public Department? Department { get; set; }

    }
}
