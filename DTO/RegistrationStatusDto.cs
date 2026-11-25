namespace DeptPick.DTO
{
    public class RegistrationStatusDto
    {
        public bool IsOpen { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime? ClosesAt { get; set; }
        public string PeriodName { get; set; } = string.Empty;
    }
}
