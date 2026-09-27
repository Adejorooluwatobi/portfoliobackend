using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities;

public class SiteSettings : BaseEntity
{
    public string SiteTitle { get; set; } = "Oluwatobi Adejoro — Senior Fullstack Software Engineer";
    public string Monogram { get; set; } = "OA";
    public string FaviconUrl { get; set; } = "assets/img/favicon2.jpg";
    public string DefaultTheme { get; set; } = "dark";
    public string CopyrightText { get; set; } = "© 2025 Oluwatobi Adejoro. All rights reserved.";
    public string FooterTagline { get; set; } = "Engineered with modern web standards, strict typing & high performance.";
    public string MetaDescription { get; set; } = "Portfolio of Oluwatobi Adejoro, a passionate and detail-oriented Fullstack Software Engineer specializing in React, Node.js, NestJS, Angular, and ASP.NET Core.";
    public string? FormspreeEndpoint { get; set; }
}
