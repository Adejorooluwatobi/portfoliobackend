using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public class Profile : BaseEntity
{
    public string FullName { get; set; } = "Oluwatobi Adejoro";
    public string PrimaryTitle { get; set; } = "Senior Fullstack Engineer";
    public string SidebarTitle { get; set; } = "Software Engineer";
    public string AvatarImageUrl { get; set; } = "assets/img/parsonal-info/oluwatobi-img.jpg";
    public string AvatarAltText { get; set; } = "Oluwatobi Adejoro - Senior Fullstack Software Engineer";
    public string Email { get; set; } = "Adejorotgold1@yahoo.com";
    public string Phone { get; set; } = "+2347080506879";
    public string PhoneDisplay { get; set; } = "+234 708 050 6879";
    public string WhatsappUrl { get; set; } = "https://wa.me/2347080506879";
    public string Location { get; set; } = "Lagos Nigeria";
    public string LocationDisplay { get; set; } = "Lagos / Remote Global";
    public DateTime? Birthday { get; set; } = new DateTime(1999, 6, 24, 0, 0, 0, DateTimeKind.Utc);
    public string CvFileUrl { get; set; } = string.Empty;
    public string CvDownloadName { get; set; } = "OLUWATOBI_Adejoro_CV.docx";
    public bool IsAvailable { get; set; } = true;
    public string AvailabilityText { get; set; } = "Available for opportunities";
    public string EmploymentStatus { get; set; } = "Open to Offers";
    public string ResponseTime { get; set; } = "< 24 Hours";
    public string EngagementScope { get; set; } = "Full-time, Contract, Technical Advisory";
    public string ResponseGuarantee { get; set; } = "Guaranteed reply within 24 hours";
    public int YearsExperience { get; set; } = 4;
    public string YearsExperienceSuffix { get; set; } = "4+";
    public string YearsExperienceLabel { get; set; } = "Production Systems";
    public int ProjectsCompleted { get; set; } = 25;
    public string ProjectsCompletedSuffix { get; set; } = "25+";
    public string ProjectsLabel { get; set; } = "Web & Cloud APIs";
}
