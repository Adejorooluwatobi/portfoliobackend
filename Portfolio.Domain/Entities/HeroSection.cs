using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public class HeroSection : BaseEntity
{
    public string CategoryBadgeText { get; set; } = "Fullstack Engineering";
    public string CategoryBadgeIcon { get; set; } = "code_blocks";
    public string Headline { get; set; } = "Architecting Scalable, User-Centric Digital Experiences";
    public string BioLead { get; set; } = "Passionate and detail-oriented Fullstack Developer with 4 years of experience in architecting, developing, and deploying robust, user-centric web applications. Proficient in both frontend and backend technologies, I specialize in crafting seamless digital experiences from concept to execution.";
    public string BioFrontend { get; set; } = "On the frontend, I leverage modern frameworks like React and Angular to translate complex design concepts into responsive, accessible, and high-performance user interfaces. My expertise ensures optimal user experience across diverse devices and browsers.";
    public string BioBackend { get; set; } = "For the backend, I possess strong capabilities in Node.js, utilizing frameworks such as Express.js and NestJS to build scalable and efficient APIs and server-side logic. Additionally, I am proficient in ASP.NET Core, enabling me to engineer dependable enterprise applications across diverse architectural patterns.";
    public string PrimaryCtaText { get; set; } = "Explore Featured Works";
    public string PrimaryCtaUrl { get; set; } = "portfolio.html";
    public string PrimaryCtaIcon { get; set; } = "layers";
    public string SecondaryCtaText { get; set; } = "Inspect Tech Stack";
    public string SecondaryCtaUrl { get; set; } = "resume.html";
    public string SecondaryCtaIcon { get; set; } = "terminal";
    public string TertiaryCtaText { get; set; } = "Track Record";
    public string TertiaryCtaUrl { get; set; } = "resume.html#experience";
    public string TertiaryCtaIcon { get; set; } = "history_edu";
}
