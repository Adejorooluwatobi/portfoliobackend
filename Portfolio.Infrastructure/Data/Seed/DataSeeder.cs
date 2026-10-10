using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Portfolio.Application.Common.Interfaces;
using Portfolio.Domain.Entities;

namespace Portfolio.Infrastructure.Data.Seed;

public class DataSeeder
{
    private readonly AppDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<DataSeeder> _logger;

    public DataSeeder(AppDbContext context, IPasswordHasher passwordHasher, ILogger<DataSeeder> logger)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task SeedAsync()
    {
        try
        {
            await SeedAdminUserAsync();
            await SeedSiteSettingsAsync();
            await SeedProfileAsync();
            await SeedSocialLinksAsync();
            await SeedHeroSectionAsync();
            await SeedPhilosophyCardsAsync();
            await SeedDisciplineCardsAsync();
            await SeedWorkExperiencesAsync();
            await SeedEducationsAsync();
            await SeedSkillsAsync();
            await SeedProjectsAsync();
            await SeedArticlesAsync();
            await SeedPageSettingsAsync();
            await SeedNavItemsAsync();

            await _context.SaveChangesAsync();
            _logger.LogInformation("Database seeded successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task SeedAdminUserAsync()
    {
        var adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL") ?? "Adejorotgold1@yahoo.com";
        var adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD") ?? "AdminPassword123!";
        var adminFullName = Environment.GetEnvironmentVariable("ADMIN_FULLNAME") ?? "Oluwatobi Adejoro";

        var existingAdmin = await _context.AdminUsers.FirstOrDefaultAsync(u => u.Email.ToLower() == adminEmail.ToLower());
        if (existingAdmin != null)
        {
            // Sync password hash with .env to ensure admin is never locked out
            existingAdmin.PasswordHash = _passwordHasher.HashPassword(adminPassword);
            existingAdmin.FullName = adminFullName;
            _context.AdminUsers.Update(existingAdmin);
            return;
        }

        var anyAdmin = await _context.AdminUsers.AnyAsync();
        if (anyAdmin) return;

        var admin = new AdminUser
        {
            Email = adminEmail,
            FullName = adminFullName,
            PasswordHash = _passwordHasher.HashPassword(adminPassword),
            Role = "Admin"
        };

        await _context.AdminUsers.AddAsync(admin);
    }

    private async Task SeedSiteSettingsAsync()
    {
        if (await _context.SiteSettings.AnyAsync()) return;

        var settings = new SiteSettings
        {
            SiteTitle = "Oluwatobi Adejoro — Senior Fullstack Software Engineer",
            Monogram = "OA",
            FaviconUrl = "assets/img/favicon2.jpg",
            DefaultTheme = "dark",
            CopyrightText = "© 2025 Oluwatobi Adejoro. All rights reserved.",
            FooterTagline = "Engineered with modern web standards, strict typing & high performance.",
            MetaDescription = "Portfolio of Oluwatobi Adejoro, a passionate and detail-oriented Fullstack Software Engineer specializing in React, Node.js, NestJS, Angular, and ASP.NET Core.",
            FormspreeEndpoint = "https://formspree.io/f/xoqrgaab"
        };

        await _context.SiteSettings.AddAsync(settings);
    }

    private async Task SeedProfileAsync()
    {
        if (await _context.Profiles.AnyAsync()) return;

        var profile = new Profile
        {
            FullName = "Oluwatobi Adejoro",
            PrimaryTitle = "Software Engineer",
            SidebarTitle = "Software Engineer",
            AvatarImageUrl = "assets/img/parsonal-info/oluwatobi-img.jpg",
            AvatarAltText = "Oluwatobi Adejoro - Senior Fullstack Software Engineer",
            Email = "Adejorotgold1@yahoo.com",
            Phone = "+2347080506879",
            PhoneDisplay = "+234 708 050 6879",
            WhatsappUrl = "https://wa.me/2347080506879",
            Location = "Lagos Nigeria",
            LocationDisplay = "Lagos / Remote Global",
            Birthday = new DateTime(1999, 6, 24, 0, 0, 0, DateTimeKind.Utc),
            CvFileUrl = Environment.GetEnvironmentVariable("INITIAL_CV_URL") ?? string.Empty,
            CvDownloadName = "OLUWATOBI_Adejoro_CV.docx",
            IsAvailable = true,
            AvailabilityText = "Available for opportunities",
            EmploymentStatus = "Open to Offers",
            ResponseTime = "< 24 Hours",
            EngagementScope = "Full-time, Contract, Technical Advisory",
            ResponseGuarantee = "Guaranteed reply within 24 hours",
            YearsExperience = 7,
            YearsExperienceSuffix = "7+",
            YearsExperienceLabel = "Production Systems",
            ProjectsCompleted = 10,
            ProjectsCompletedSuffix = "10+",
            ProjectsLabel = "Web & Cloud APIs"
        };

        await _context.Profiles.AddAsync(profile);
    }

    private async Task SeedSocialLinksAsync()
    {
        if (await _context.SocialLinks.AnyAsync()) return;

        var socials = new List<SocialLink>
        {
            new() { Platform = "github", Title = "GitHub", Url = "https://github.com/Adejorooluwatobi", Icon = "terminal", ShowInHeader = true, ShowInFooter = true, ShowInSidebar = false, SortOrder = 1 },
            new() { Platform = "linkedin", Title = "LinkedIn", Url = "https://www.linkedin.com/in/adejoro-oluwatobi-6009411b5/", Icon = "share", ShowInHeader = true, ShowInFooter = true, ShowInSidebar = true, SortOrder = 2 },
            new() { Platform = "twitter", Title = "Twitter / X", Url = "https://x.com/tgold_adejoro", Icon = "chat", ShowInHeader = false, ShowInFooter = true, ShowInSidebar = true, SortOrder = 3 },
            new() { Platform = "facebook", Title = "Facebook", Url = "https://web.facebook.com/oluwatobi.adejoro.2025", Icon = "facebook", ShowInHeader = false, ShowInFooter = false, ShowInSidebar = true, SortOrder = 4 },
            new() { Platform = "instagram", Title = "Instagram", Url = "https://www.instagram.com/oluwa_tgold/", Icon = "instagram", ShowInHeader = false, ShowInFooter = false, ShowInSidebar = true, SortOrder = 5 }
        };

        await _context.SocialLinks.AddRangeAsync(socials);
    }

    private async Task SeedHeroSectionAsync()
    {
        if (await _context.HeroSections.AnyAsync()) return;

        var hero = new HeroSection
        {
            CategoryBadgeText = "Software Engineer ",
            CategoryBadgeIcon = "code_blocks",
            Headline = "Architecting Scalable, User-Centric Digital Experiences",
            BioLead = "Software engineer with 7+ years of experience, specializing in backend development with ASP.NET Core and C#, alongside \nstrong Node.js/NestJS expertise. I build RESTful APIs and scalable backend systems using .NET 6/7/8, EF Core, and SQL \nServer/PostgreSQL, applying SOLID principles, OOP, dependency injection, and async/await patterns to keep codebases clean \nand testable. \nI'm equally comfortable across the full stack -- integrating backend services with React and Angular frontends -- and have shipped \nwork spanning fintech, SaaS, and public sector platforms. I'm experienced with Git-based workflows, pull requests, code review, \nand unit testing (xUnit, NUnit, Moq, Jest), and have deployed and managed services on Azure, AWS, and GCP. ",
            BioFrontend = "On the frontend, I leverage modern frameworks like React and Angular to translate complex design concepts into responsive, accessible, and high-performance user interfaces. My expertise ensures optimal user experience across diverse devices and browsers.",
            BioBackend = "For the backend, I possess strong capabilities in Node.js, utilizing frameworks such as Express.js and NestJS to build scalable and efficient APIs and server-side logic. Additionally, I am proficient in ASP.NET Core, enabling me to engineer dependable enterprise applications across diverse architectural patterns.",
            PrimaryCtaText = "Explore Featured Works",
            PrimaryCtaUrl = "portfolio.html",
            PrimaryCtaIcon = "layers",
            SecondaryCtaText = "Inspect Tech Stack",
            SecondaryCtaUrl = "resume.html",
            SecondaryCtaIcon = "terminal",
            TertiaryCtaText = "Track Record",
            TertiaryCtaUrl = "resume.html#experience",
            TertiaryCtaIcon = "history_edu"
        };

        await _context.HeroSections.AddAsync(hero);
    }

    private async Task SeedPhilosophyCardsAsync()
    {
        if (await _context.PhilosophyCards.AnyAsync()) return;

        var cards = new List<PhilosophyCard>
        {
            new() { Icon = "speed", Title = "Performance First", Description = "Sub-second latencies, tree-shaken bundles, optimized SQL queries & Redis caching.", SortOrder = 1, AccentColor = "#8b5cf6" },
            new() { Icon = "security", Title = "Enterprise Grade", Description = "Strict typing, JWT/OAuth2 mechanisms, role-based access, and clean domain design.", SortOrder = 2, AccentColor = "#10b981" },
            new() { Icon = "devices", Title = "Fluid UI / UX", Description = "Responsive precision, accessible semantic layouts, and tactile motion design.", SortOrder = 3, AccentColor = "#0ea5e9" }
        };

        await _context.PhilosophyCards.AddRangeAsync(cards);
    }

    private async Task SeedDisciplineCardsAsync()
    {
        if (await _context.DisciplineCards.AnyAsync()) return;

        var c1 = new DisciplineCard
        {
            IndexTag = "01 // FRONTEND",
            Icon = "palette",
            Title = "UI / UX Engineering",
            Description = "My comprehensive skill set spans HTML, modern CSS, JavaScript, and encompasses strong UI/UX principles, allowing me to bridge the gap between design and development effectively. I am dedicated to optimizing application performance, ensuring cross-browser compatibility, and maintaining clean, maintainable code.",
            AccentColor = "#8b5cf6",
            SortOrder = 1,
            Tags = new List<DisciplineCardTag>
            {
                new() { TagName = "React 18", SortOrder = 1 },
                new() { TagName = "Angular 16", SortOrder = 2 },
                new() { TagName = "Tailwind CSS", SortOrder = 3 },
                new() { TagName = "Accessibility (a11y)", SortOrder = 4 }
            }
        };

        var c2 = new DisciplineCard
        {
            IndexTag = "02 // SYSTEMS",
            Icon = "widgets",
            Title = "Fullstack App Development",
            Description = "I contribute engineering expertise to innovative projects within growth-oriented organizations, collaborating with cross-functional teams to deliver impactful solutions and continuously enhance user experiences. My commitment to continuous learning drives my pursuit of engineering excellence in every deployment.",
            AccentColor = "#10b981",
            SortOrder = 2,
            Tags = new List<DisciplineCardTag>
            {
                new() { TagName = "Micro-frontends", SortOrder = 1 },
                new() { TagName = "TypeScript", SortOrder = 2 },
                new() { TagName = "Next.js", SortOrder = 3 },
                new() { TagName = "State Architecture", SortOrder = 4 },
                new() { TagName = "Angular.js", SortOrder = 5 }
            }
        };

        var c3 = new DisciplineCard
        {
            IndexTag = "03 // INFRASTRUCTURE",
            Icon = "dns",
            Title = "API & Backend Architecture",
            Description = "Possessing deep capabilities in Node.js, utilizing Express.js and NestJS to build scalable, fault-tolerant microservices and asynchronous queue pipelines. Concurrently proficient in ASP.NET Core for building secure enterprise data endpoints and multi-tier business logic engines.",
            AccentColor = "#0ea5e9",
            SortOrder = 3,
            Tags = new List<DisciplineCardTag>
            {
                new() { TagName = "NestJS", SortOrder = 1 },
                new() { TagName = "REST & GraphQL", SortOrder = 2 },
                new() { TagName = "Node.js / Express", SortOrder = 3 },
                new() { TagName = "ASP.NET Core", SortOrder = 4 },
                new() { TagName = "Fastapi", SortOrder = 5 },
                new() { TagName = "Postgresql", SortOrder = 6 },
                new() { TagName = "SQL", SortOrder = 7 },
                new() { TagName = "database", SortOrder = 8 },
                new() { TagName = "Mongodb", SortOrder = 9 }
            }
        };

        var c4 = new DisciplineCard
        {
            IndexTag = "04 // OPTIMIZATION",
            Icon = "rocket_launch",
            Title = "Performance & Code Quality",
            Description = "Engineering applications that load fast, scale gracefully, and minimize operational costs. Dedicated to automated testing, continuous integration pipelines, cross-browser compatibility, and rigorous code review standards that eliminate technical debt before deployment.",
            AccentColor = "#f59e0b",
            SortOrder = 4,
            Tags = new List<DisciplineCardTag>
            {
                new() { TagName = "CI / CD Automation", SortOrder = 1 },
                new() { TagName = "Docker Containers", SortOrder = 2 },
                new() { TagName = "Core Web Vitals", SortOrder = 3 },
                new() { TagName = "Jest / Integration", SortOrder = 4 }
            }
        };

        await _context.DisciplineCards.AddRangeAsync(c1, c2, c3, c4);
    }

    private async Task SeedWorkExperiencesAsync()
    {
        if (await _context.WorkExperiences.AnyAsync()) return;

        var e1 = new WorkExperience
        {
            JobTitle = "Software Engineer",
            CompanyName = "TOMA Legal (TOMA Tech Ltd)",
            EmploymentType = "Remote",
            DateRange = "2026 — Present",
            IsCurrent = true,
            Description = "● Build and maintain backend services in C# / ASP.NET Core for legal-tech document management and precedent library \nproducts. \n● Integrate backend APIs with React and Angular frontends, ensuring smooth end-to-end data flow across the stack. \n● Apply SOLID and OOP principles with clean architecture to keep the codebase modular and easy to extend. \n● Collaborate with the team through Git-based workflows, pull requests, and code review.",
            AccentVariant = "primary",
            SortOrder = 1,
            Technologies = new List<ExperienceTechnology>
            {
                new() { Name = "React.js", SortOrder = 1 },
                new() { Name = "CI/CD", SortOrder = 2 },
                new() { Name = "RabbitMQ", SortOrder = 3 },
                new() { Name = "ASP.NET", SortOrder = 4 },
                new() { Name = "Reddis", SortOrder = 5 },
                new() { Name = "Next.js", SortOrder = 6 },
                new() { Name = "Docker", SortOrder = 7 }
            }
        };

        var e2 = new WorkExperience
        {
            JobTitle = "Senior Software Developer",
            CompanyName = "Cytech Consult",
            EmploymentType = "Full-Time",
            DateRange = "2024 — Present",
            IsCurrent = true,
            Description = "Leading the architectural design, modernization, and development of responsive cloud web applications with React, NestJS, and ASP.NET Core. Spearheaded performance optimizations reducing initial bundle payloads by 38%, established automated CI/CD deployment pipelines, and instituted code quality standards.",
            AccentVariant = "primary",
            SortOrder = 2,
            Technologies = new List<ExperienceTechnology>
            {
                new() { Name = "React 18", SortOrder = 1 },
                new() { Name = "NestJS", SortOrder = 2 },
                new() { Name = "ASP.NET Core", SortOrder = 3 },
                new() { Name = "PostgreSQL", SortOrder = 4 },
                new() { Name = "CI / CD", SortOrder = 5 }
            }
        };

        var e3 = new WorkExperience
        {
            JobTitle = "Endpoint Tester",
            CompanyName = "Ike Qubicle Project",
            EmploymentType = "Contract",
            DateRange = "2024 — 2025",
            IsCurrent = false,
            Description = "Conducted comprehensive API endpoint testing, load profiling, and payload validation for distributed backend microservices. Identified race conditions, secured authorization guardrails (JWT/OAuth2), and verified edge-case resilience prior to staging and production rollouts.",
            AccentVariant = "secondary",
            SortOrder = 3,
            Technologies = new List<ExperienceTechnology>
            {
                new() { Name = "API Testing", SortOrder = 1 },
                new() { Name = "Postman", SortOrder = 2 },
                new() { Name = "Security Validation", SortOrder = 3 },
                new() { Name = "Regression Suites", SortOrder = 4 }
            }
        };

        var e4 = new WorkExperience
        {
            JobTitle = "Mid Level Fullstack Web Developer",
            CompanyName = "Cytech Consult",
            EmploymentType = "Full-Time",
            DateRange = "2022 — 2023",
            IsCurrent = false,
            Description = "Engineered fullstack features spanning Node.js/Express backends and Angular/React SPAs. Integrated third-party payment gateways, designed relational database schemas, and ensured adherence to WCAG accessibility guidelines.",
            AccentVariant = "neutral",
            SortOrder = 4,
            Technologies = new List<ExperienceTechnology>
            {
                new() { Name = "Node.js", SortOrder = 1 },
                new() { Name = "Angular", SortOrder = 2 },
                new() { Name = "Express.js", SortOrder = 3 },
                new() { Name = "MongoDB", SortOrder = 4 },
                new() { Name = "SQL", SortOrder = 5 }
            }
        };

        var e5 = new WorkExperience
        {
            JobTitle = "Jr. Frontend Web Developer",
            CompanyName = "Cytech Consult",
            EmploymentType = "Full-Time",
            DateRange = "2019 — 2022",
            IsCurrent = false,
            Description = "Transformed Figma and Adobe XD prototypes into responsive, pixel-accurate HTML, CSS, and modern JavaScript applications. Collaborated closely with design teams to ensure flawless cross-browser compatibility and snappy animations.",
            AccentVariant = "neutral",
            SortOrder = 5,
            Technologies = new List<ExperienceTechnology>
            {
                new() { Name = "JavaScript ES6+", SortOrder = 1 },
                new() { Name = "CSS3 / SASS", SortOrder = 2 },
                new() { Name = "UI/UX Principles", SortOrder = 3 },
                new() { Name = "Bootstrap", SortOrder = 4 }
            }
        };

        await _context.WorkExperiences.AddRangeAsync(e1, e2, e3, e4, e5);
    }

    private async Task SeedEducationsAsync()
    {
        if (await _context.Educations.AnyAsync()) return;

        var edus = new List<Education>
        {
            new()
            {
                DegreeTitle = "B.Sc. Computer Science",
                InstitutionName = "National Open University of Nigeria (NOUN)",
                Location = "Surulere, Lagos",
                DateRange = "2021 — 2024",
                Icon = "school",
                Description = "Rigorous coursework in software engineering, algorithms, database architecture, distributed systems, and computer networks.",
                CredentialType = "Degree",
                SortOrder = 1
            },
            new()
            {
                DegreeTitle = "FullStack JavaScript",
                InstitutionName = "Cytech Technical Institute",
                Location = "Lagos, Nigeria",
                DateRange = "2019 — 2020",
                Icon = "terminal",
                Description = "Intensive practical curriculum covering ES6+, Node.js runtime, asynchronous state patterns, DOM APIs, and scalable fullstack web development.",
                CredentialType = "Certification",
                SortOrder = 2
            }
        };

        await _context.Educations.AddRangeAsync(edus);
    }

    private async Task SeedSkillsAsync()
    {
        if (await _context.SkillCategories.AnyAsync()) return;

        var catFrontend = new SkillCategory
        {
            Title = "Frontend Ecosystem",
            Subtitle = "Client interfaces & interaction",
            Icon = "web",
            AccentColorToken = "primary",
            SortOrder = 1,
            Skills = new List<SkillItem>
            {
                new() { Name = "React.js & Next.js", ProficiencyPercent = 95, IsPrimary = true, SortOrder = 1 },
                new() { Name = "Angular Framework", ProficiencyPercent = 88, IsPrimary = true, SortOrder = 2 },
                new() { Name = "TypeScript / ESNext", ProficiencyPercent = 92, IsPrimary = true, SortOrder = 3 },
                new() { Name = "Tailwind CSS & Modern UI", ProficiencyPercent = 94, IsPrimary = true, SortOrder = 4 },
                new() { Name = "Redux Toolkit", IsPrimary = false, SortOrder = 5 },
                new() { Name = "RxJS", IsPrimary = false, SortOrder = 6 },
                new() { Name = "HTML5 Semantic", IsPrimary = false, SortOrder = 7 },
                new() { Name = "CSS Modules", IsPrimary = false, SortOrder = 8 }
            }
        };

        var catBackend = new SkillCategory
        {
            Title = "Backend & APIs",
            Subtitle = "Distributed services & logic",
            Icon = "database",
            AccentColorToken = "secondary",
            SortOrder = 2,
            Skills = new List<SkillItem>
            {
                new() { Name = "Node.js & Express.js", ProficiencyPercent = 94, IsPrimary = true, SortOrder = 1 },
                new() { Name = "NestJS Architecture", ProficiencyPercent = 90, IsPrimary = true, SortOrder = 2 },
                new() { Name = "ASP.NET Core (.NET 8/10)", ProficiencyPercent = 86, IsPrimary = true, SortOrder = 3 },
                new() { Name = "REST & GraphQL Protocols", ProficiencyPercent = 92, IsPrimary = true, SortOrder = 4 },
                new() { Name = "Microservices", IsPrimary = false, SortOrder = 5 },
                new() { Name = "Entity Framework", IsPrimary = false, SortOrder = 6 },
                new() { Name = "JWT / OAuth2", IsPrimary = false, SortOrder = 7 },
                new() { Name = "Prisma / TypeORM", IsPrimary = false, SortOrder = 8 }
            }
        };

        var catCloud = new SkillCategory
        {
            Title = "Data & Cloud Infra",
            Subtitle = "Storage, cache & deployment",
            Icon = "cloud_sync",
            AccentColorToken = "tertiary",
            SortOrder = 3,
            Skills = new List<SkillItem>
            {
                new() { Name = "PostgreSQL & SQL Server", ProficiencyPercent = 90, IsPrimary = true, SortOrder = 1 },
                new() { Name = "MongoDB (NoSQL)", ProficiencyPercent = 88, IsPrimary = true, SortOrder = 2 },
                new() { Name = "Docker & Containers", ProficiencyPercent = 82, IsPrimary = true, SortOrder = 3 },
                new() { Name = "Git, CI/CD & Testing", ProficiencyPercent = 91, IsPrimary = true, SortOrder = 4 },
                new() { Name = "Redis Cache", IsPrimary = false, SortOrder = 5 },
                new() { Name = "GitHub Actions", IsPrimary = false, SortOrder = 6 },
                new() { Name = "AWS / Vercel", IsPrimary = false, SortOrder = 7 },
                new() { Name = "Linux / Nginx", IsPrimary = false, SortOrder = 8 }
            }
        };

        await _context.SkillCategories.AddRangeAsync(catFrontend, catBackend, catCloud);
    }

    private async Task SeedProjectsAsync()
    {
        if (await _context.ProjectCategories.AnyAsync()) return;

        var catAll = new ProjectCategory { Slug = "all", Label = "All", SortOrder = 1 };
        var catFullstack = new ProjectCategory { Slug = "fullstack", Label = "Fullstack", SortOrder = 2 };
        var catFrontend = new ProjectCategory { Slug = "frontend", Label = "Frontend", SortOrder = 3 };
        var catAiBackend = new ProjectCategory { Slug = "ai-backend", Label = "AI & Backend", SortOrder = 4 };

        await _context.ProjectCategories.AddRangeAsync(catAll, catFullstack, catFrontend, catAiBackend);

        var p1 = new Project
        {
            Slug = "tslhub",
            Title = "TSL Hub Law Firm Platform",
            ClientName = "TSL Legal Practitioners",
            CategoryBadgeText = "Legal Tech • Fullstack",
            Timeframe = "2023 — 2024",
            ShortDescription = "Corporate legal practice digital ecosystem engineered for TSL Hub. Delivers responsive practice area directories, client consultation inquiry workflows, accessible typography, and SEO-optimized web performance.",
            ImageUrl = "assets/img/work/logo (1).png",
            ImageAlt = "TSL Hub Law Firm Platform Logo",
            LiveUrl = "https://tslhub.org/index.html",
            HasCaseStudy = true,
            IsPublished = true,
            SortOrder = 1,
            Tags = new List<ProjectTag>
            {
                new() { TagName = "HTML5 / Semantic", SortOrder = 1 },
                new() { TagName = "CSS3 / SASS", SortOrder = 2 },
                new() { TagName = "JavaScript", SortOrder = 3 },
                new() { TagName = "Responsive UI", SortOrder = 4 },
                new() { TagName = "SEO Tuning", SortOrder = 5 }
            },
            CategoryMaps = new List<ProjectCategoryMap>
            {
                new() { ProjectCategory = catFullstack },
                new() { ProjectCategory = catFrontend }
            },
            CaseStudy = new CaseStudy
            {
                Title = "TSL Hub Law Firm Platform",
                CategoryLabel = "Fullstack / Legal Tech",
                Year = "2023 — 2024",
                ClientName = "TSL Legal Practitioners",
                HeroImageUrl = "assets/img/work/logo (1).png",
                Summary = "Comprehensive corporate web platform and digital service gateway for TSL Hub law firm. Designed with high-performance responsive frontend, accessibility compliance, service directory, and client consultation booking architecture.",
                LiveUrl = "https://tslhub.org/index.html",
                Highlights = new List<CaseStudyHighlight>
                {
                    new() { HighlightText = "Responsive client portal with fluid transitions and legal advisory categories", SortOrder = 1 },
                    new() { HighlightText = "SEO-optimized architecture achieving high Google Lighthouse performance scores", SortOrder = 2 },
                    new() { HighlightText = "Integrated appointment inquiry and corporate communication workflows", SortOrder = 3 },
                    new() { HighlightText = "Cross-browser and mobile device compatibility testing across all resolutions", SortOrder = 4 }
                },
                Technologies = new List<CaseStudyTechnology>
                {
                    new() { Name = "HTML5", SortOrder = 1 },
                    new() { Name = "Modern CSS3", SortOrder = 2 },
                    new() { Name = "JavaScript", SortOrder = 3 },
                    new() { Name = "Responsive UI/UX", SortOrder = 4 },
                    new() { Name = "SEO Optimization", SortOrder = 5 }
                }
            }
        };

        var p2 = new Project
        {
            Slug = "comza",
            Title = "Comza Africa Commercial Ecosystem",
            ClientName = "Comza Africa",
            CategoryBadgeText = "Commercial Ecosystem",
            Timeframe = "2023 — 2024",
            ShortDescription = "Pan-African commercial venture digital ecosystem built to present multi-sector trading, modern logistics, and investment opportunities with high visual authority and responsive customer interaction channels.",
            ImageUrl = "assets/img/work/comza.png",
            ImageAlt = "Comza Africa Platform Logo",
            LiveUrl = "https://www.comzafrica.com/index.html",
            HasCaseStudy = true,
            IsPublished = true,
            SortOrder = 2,
            Tags = new List<ProjectTag>
            {
                new() { TagName = "JavaScript ES6+", SortOrder = 1 },
                new() { TagName = "Modern UI/UX", SortOrder = 2 },
                new() { TagName = "Interactive Catalog", SortOrder = 3 },
                new() { TagName = "Responsive Layout", SortOrder = 4 }
            },
            CategoryMaps = new List<ProjectCategoryMap>
            {
                new() { ProjectCategory = catFullstack },
                new() { ProjectCategory = catFrontend }
            },
            CaseStudy = new CaseStudy
            {
                Title = "Comza Africa Commercial Ecosystem",
                CategoryLabel = "Fullstack Commercial Platform",
                Year = "2023 — 2024",
                ClientName = "Comza Africa",
                HeroImageUrl = "assets/img/work/comza.png",
                Summary = "Pan-African commercial venture digital ecosystem built to present multi-sector trading, modern logistics, and investment opportunities with modern branding, high visual authority, and seamless customer interaction channels.",
                LiveUrl = "https://www.comzafrica.com/index.html",
                Highlights = new List<CaseStudyHighlight>
                {
                    new() { HighlightText = "High-conversion corporate showcase engineered for mobile-first African and global traffic", SortOrder = 1 },
                    new() { HighlightText = "Dynamic product & service discovery layout with optimized media delivery", SortOrder = 2 },
                    new() { HighlightText = "Integrated direct client lead routing with responsive contact validation", SortOrder = 3 },
                    new() { HighlightText = "Sub-second first-contentful paint performance tuning", SortOrder = 4 }
                },
                Technologies = new List<CaseStudyTechnology>
                {
                    new() { Name = "JavaScript ES6+", SortOrder = 1 },
                    new() { Name = "Modern UI/UX", SortOrder = 2 },
                    new() { Name = "CSS Architecture", SortOrder = 3 },
                    new() { Name = "Performance Optimization", SortOrder = 4 }
                }
            }
        };

        var p3 = new Project
        {
            Slug = "aitoolkit",
            Title = "AI Productivity Toolkit (Doyin Chris)",
            ClientName = "Doyin Chris Solutions",
            CategoryBadgeText = "AI & Web Application",
            Timeframe = "2024",
            ShortDescription = "Dark-themed AI productivity suite providing automated prompt engineering tools, content synthesis helpers, and tactile utility widgets designed for high-focus digital workflows.",
            ImageUrl = "assets/img/work/AI-Powerd.jpg",
            ImageAlt = "AI Productivity Toolkit",
            LiveUrl = "https://doyincl.github.io/Ai/",
            HasCaseStudy = true,
            IsPublished = true,
            SortOrder = 3,
            Tags = new List<ProjectTag>
            {
                new() { TagName = "AI Workflows", SortOrder = 1 },
                new() { TagName = "JavaScript", SortOrder = 2 },
                new() { TagName = "Tactile Dark UI", SortOrder = 3 },
                new() { TagName = "Modern CSS", SortOrder = 4 }
            },
            CategoryMaps = new List<ProjectCategoryMap>
            {
                new() { ProjectCategory = catAiBackend },
                new() { ProjectCategory = catFrontend }
            },
            CaseStudy = new CaseStudy
            {
                Title = "AI Productivity Toolkit",
                CategoryLabel = "AI & Fullstack Application",
                Year = "2024",
                ClientName = "Doyin Chris Solutions",
                HeroImageUrl = "assets/img/work/AI-Powerd.jpg",
                Summary = "Modern AI-driven productivity dashboard enabling intelligent content synthesis, workflow acceleration, and automated prompt engineering tools in a dark-themed user interface.",
                LiveUrl = "https://doyincl.github.io/Ai/",
                Highlights = new List<CaseStudyHighlight>
                {
                    new() { HighlightText = "Interactive AI utility widgets engineered with responsive modern styling", SortOrder = 1 },
                    new() { HighlightText = "Client-side state management for prompt generation and output caching", SortOrder = 2 },
                    new() { HighlightText = "Intuitive tactile dark UI with neon glow indicators and instant feedback", SortOrder = 3 },
                    new() { HighlightText = "Extensible component system ready for enterprise LLM endpoint integrations", SortOrder = 4 }
                },
                Technologies = new List<CaseStudyTechnology>
                {
                    new() { Name = "AI Integrations", SortOrder = 1 },
                    new() { Name = "JavaScript", SortOrder = 2 },
                    new() { Name = "Responsive UI", SortOrder = 3 },
                    new() { Name = "Modern CSS", SortOrder = 4 },
                    new() { Name = "API Client", SortOrder = 5 }
                }
            }
        };

        var p4 = new Project
        {
            Slug = "gateway",
            Title = "Distributed Microservices Gateway",
            ClientName = "Oluwatobi Adejoro",
            CategoryBadgeText = "Cloud Architecture",
            Timeframe = "2023 — 2024",
            ShortDescription = "Central reverse proxy and authentication gateway engineered with Node.js and NestJS. Implements token rotation (JWT), rate limiting, payload validation, and sub-50ms caching using Redis and PostgreSQL.",
            IconKey = "dns",
            GithubUrl = "https://github.com",
            ArticleUrl = "articles.html",
            HasCaseStudy = false,
            IsPublished = true,
            SortOrder = 4,
            Tags = new List<ProjectTag>
            {
                new() { TagName = "NestJS", SortOrder = 1 },
                new() { TagName = "Node.js", SortOrder = 2 },
                new() { TagName = "Redis Caching", SortOrder = 3 },
                new() { TagName = "PostgreSQL", SortOrder = 4 },
                new() { TagName = "Docker", SortOrder = 5 }
            },
            CategoryMaps = new List<ProjectCategoryMap>
            {
                new() { ProjectCategory = catAiBackend },
                new() { ProjectCategory = catFullstack }
            }
        };

        await _context.Projects.AddRangeAsync(p1, p2, p3, p4);
    }

    private async Task SeedArticlesAsync()
    {
        if (await _context.Articles.AnyAsync()) return;

        var a1 = new Article
        {
            Slug = "nestjs-ecommerce-endpoints",
            Title = "Creating Scalable E-Commerce Endpoints Using Node.js & NestJS Technology",
            Excerpt = "A comprehensive architectural walkthrough examining how to architect robust, type-safe e-commerce endpoints with NestJS. Covers dependency injection, payload validation pipes, controller-service separation, and database transaction consistency.",
            Category = "Backend & NestJS",
            PublicationType = "Software Development Guide",
            PublishStatus = "Published",
            ReadTimeMinutes = 6,
            ImageUrl = "/uploads/portfolio/articles/blog-img3_1b66dacf005b4d6e9155c9ceeb9d93fa.png",
            ImageAlt = "NestJS E-commerce architecture guide",
            LinkedinUrl = "https://bit.ly/46uFXSL",
            TwitterUrl = "https://x.com/tgold_adejoro/status/1940115330845876375",
            FooterAnnotation = "bit.ly/46uFXSL",
            PublishedAt = new DateTime(2026, 6, 28, 0, 0, 0, DateTimeKind.Utc),
            IsActive = true,
            SortOrder = 1,
            Tags = new List<ArticleTag>
            {
                new() { TagName = "NestJS", SortOrder = 1 },
                new() { TagName = "Node.js", SortOrder = 2 },
                new() { TagName = "TypeScript", SortOrder = 3 },
                new() { TagName = "REST APIs", SortOrder = 4 }
            },
            Links = new List<ArticleLink>
            {
                new() { Title = "LinkedIn", Url = "https://bit.ly/46uFXSL", Icon = "share", SortOrder = 1 },
                new() { Title = "X / Thread", Url = "https://x.com/tgold_adejoro/status/1940115330845876375", Icon = "chat", SortOrder = 2 }
            }
        };

        var a2 = new Article
        {
            Slug = "enterprise-typescript-architecture",
            Title = "Enterprise TypeScript & Modular Architecture Patterns in Production Systems",
            Excerpt = "Key insights on implementing clean interfaces, strict typing boundaries, and modular domain architecture. Explores how domain-driven design principles help engineering teams reduce runtime exceptions and maintain long-term velocity.",
            Category = "Architecture & Patterns",
            PublicationType = "LinkedIn Technical Post",
            PublishStatus = "Published",
            ReadTimeMinutes = 5,
            ImageUrl = "assets/img/blog/blog-img3.png",
            ImageAlt = "TypeScript Architecture Discussion",
            LinkedinUrl = "https://www.linkedin.com/posts/adejoro-oluwatobi-6009411b5_softwaredevelopment-nestjs-typescript-activity-7377137213349707776-I7SF",
            TwitterUrl = "https://x.com/tgold_adejoro/status/1971370634048569808",
            FooterAnnotation = "Published Post",
            PublishedAt = new DateTime(2026, 8, 28, 16, 23, 57, DateTimeKind.Utc),
            IsActive = true,
            SortOrder = 2,
            Tags = new List<ArticleTag>
            {
                new() { TagName = "TypeScript", SortOrder = 1 },
                new() { TagName = "Clean Architecture", SortOrder = 2 },
                new() { TagName = "Design Patterns", SortOrder = 3 },
                new() { TagName = "Best Practices", SortOrder = 4 }
            },
            Links = new List<ArticleLink>
            {
                new() { Title = "LinkedIn", Url = "https://www.linkedin.com/posts/adejoro-oluwatobi-6009411b5_softwaredevelopment-nestjs-typescript-activity-7377137213349707776-I7SF", Icon = "share", SortOrder = 1 },
                new() { Title = "X / Thread", Url = "https://x.com/tgold_adejoro/status/1971370634048569808", Icon = "chat", SortOrder = 2 }
            }
        };

        await _context.Articles.AddRangeAsync(a1, a2);
    }

    private async Task SeedPageSettingsAsync()
    {
        if (await _context.PageSettings.AnyAsync()) return;

        var pages = new List<PageSettings>
        {
            new()
            {
                PageKey = "index",
                SeoTitle = "Oluwatobi Adejoro — Senior Fullstack Software Engineer",
                SeoDescription = "Portfolio of Oluwatobi Adejoro, a passionate and detail-oriented Fullstack Software Engineer specializing in React, Node.js, NestJS, Angular, and ASP.NET Core.",
                HeroHeading = "Architecting Scalable, User-Centric Digital Experiences"
            },
            new()
            {
                PageKey = "resume",
                SeoTitle = "Resume & Technical Skills — Oluwatobi Adejoro",
                SeoDescription = "Professional experience, career timeline, formal education, and technical ecosystem of Oluwatobi Adejoro, Senior Fullstack Software Engineer.",
                HeroBadgeIcon = "history_edu",
                HeroBadgeText = "Professional Track Record",
                HeroHeading = "Experience, Credentials & Skills",
                HeroSubtitle = "7+ continuous years engineering production web applications, distributed APIs, microservices, and responsive user interfaces."
            },
            new()
            {
                PageKey = "portfolio",
                SeoTitle = "Featured Works & Projects — Oluwatobi Adejoro",
                SeoDescription = "Production applications, client ecosystems, and web systems engineered by Oluwatobi Adejoro, Senior Fullstack Software Engineer.",
                HeroBadgeIcon = "verified",
                HeroBadgeText = "Engineered Implementations",
                HeroHeading = "Featured Real-World Works",
                HeroSubtitle = "Verified client platforms, scalable web architectures, and production systems engineered for responsive reliability."
            },
            new()
            {
                PageKey = "articles",
                SeoTitle = "Technical Articles & Engineering Insights — Oluwatobi Adejoro",
                SeoDescription = "Technical publications, backend engineering guides, and software architecture articles written by Oluwatobi Adejoro.",
                HeroBadgeIcon = "edit_note",
                HeroBadgeText = "Knowledge Sharing",
                HeroHeading = "Technical Writing & Architecture",
                HeroSubtitle = "In-depth articles covering backend microservices, modular NestJS architecture, TypeScript patterns, and high-performance web engineering.",
                CalloutHeadline = "Have a technical project or inquiry in mind?",
                CalloutBody = "I am available for full-time engineering roles, contract development, and system consulting.",
                CalloutButtonText = "Let's Talk",
                CalloutButtonHref = "contact.html"
            },
            new()
            {
                PageKey = "contact",
                SeoTitle = "Contact & Inquiries — Oluwatobi Adejoro",
                SeoDescription = "Get in touch with Oluwatobi Adejoro, Senior Fullstack Software Engineer. Inquire about full-time engineering roles, freelance contracts, or technical advisory.",
                HeroBadgeText = "Available for work",
                HeroHeading = "Let's Engineer Something Remarkable",
                HeroSubtitle = "Whether you are looking for a senior fullstack engineer for a full-time role, a robust microservices API architect, or need high-performance web consulting, my inbox is open."
            }
        };

        await _context.PageSettings.AddRangeAsync(pages);
    }

    private async Task SeedNavItemsAsync()
    {
        if (await _context.NavItems.AnyAsync()) return;

        var items = new List<NavItem>
        {
            new() { Label = "About", MobileLabel = "About Overview", Url = "index.html", Icon = "person", DataPage = "index.html", SortOrder = 1 },
            new() { Label = "Resume & Skills", MobileLabel = "Resume & Skills", Url = "resume.html", Icon = "description", DataPage = "resume.html", SortOrder = 2 },
            new() { Label = "Featured Works", MobileLabel = "Featured Works", Url = "portfolio.html", Icon = "layers", DataPage = "portfolio.html", SortOrder = 3 },
            new() { Label = "Articles", MobileLabel = "Articles & Insights", Url = "articles.html", Icon = "edit_note", DataPage = "articles.html", SortOrder = 4 },
            new() { Label = "Contact", MobileLabel = "Contact Inquiries", Url = "contact.html", Icon = "mail", DataPage = "contact.html", SortOrder = 5 }
        };

        await _context.NavItems.AddRangeAsync(items);
    }
}
