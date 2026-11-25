namespace DeptPick.DTO
{
    public class CollegeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Status { get; set; } // 0=NotConfigured, 1=available
    }
}
