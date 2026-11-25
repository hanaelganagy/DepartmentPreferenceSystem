namespace DeptPick.DTO
{
    // الكلاس اللي الـ Flutter هيبعت منه الرغبات
    public class SaveDraftDto
    {
        public List<int> DepartmentIds { get; set; } = new List<int>();
    }

    // الكلاس اللي السيرفر هيرجع منه شكل المسودة
    public class DraftResponseDto
    {
        public int SubmissionId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; }
        public List<PreferenceItemDto> Preferences { get; set; } = new List<PreferenceItemDto>();
    }
    public class PreferenceItemDto
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public int Priority { get; set; }
    }
}
