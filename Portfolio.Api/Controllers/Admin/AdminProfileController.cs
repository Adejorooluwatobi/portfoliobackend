using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Admin;
using Portfolio.Application.Services;

namespace Portfolio.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/[controller]")]
[Authorize]
public class AdminProfileController : ControllerBase
{
    private readonly IPortfolioAdminService _adminService;

    public AdminProfileController(IPortfolioAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        var profile = await _adminService.GetProfileAsync(cancellationToken);
        return Ok(profile);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequestDto dto, CancellationToken cancellationToken)
    {
        var success = await _adminService.UpdateProfileAsync(dto, cancellationToken);
        return Ok(new { success, message = "Profile updated successfully." });
    }

    // Socials
    [HttpGet("socials")]
    public async Task<IActionResult> GetSocials(CancellationToken cancellationToken)
    {
        var socials = await _adminService.GetSocialLinksAsync(cancellationToken);
        return Ok(socials);
    }

    [HttpPost("socials")]
    public async Task<IActionResult> CreateSocial([FromBody] SocialLinkCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var created = await _adminService.CreateSocialLinkAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetSocials), new { id = created.Id }, created);
    }

    [HttpPut("socials/{id}")]
    public async Task<IActionResult> UpdateSocial(Guid id, [FromBody] SocialLinkCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var success = await _adminService.UpdateSocialLinkAsync(id, dto, cancellationToken);
        if (!success) return NotFound(new { message = "Social link not found." });
        return Ok(new { success, message = "Social link updated successfully." });
    }

    [HttpDelete("socials/{id}")]
    public async Task<IActionResult> DeleteSocial(Guid id, CancellationToken cancellationToken)
    {
        var success = await _adminService.DeleteSocialLinkAsync(id, cancellationToken);
        if (!success) return NotFound(new { message = "Social link not found." });
        return Ok(new { success, message = "Social link deleted successfully." });
    }

    // Hero Section
    [HttpGet("hero")]
    public async Task<IActionResult> GetHero(CancellationToken cancellationToken)
    {
        var hero = await _adminService.GetHeroSectionAsync(cancellationToken);
        return Ok(hero);
    }

    [HttpPut("hero")]
    public async Task<IActionResult> UpdateHero([FromBody] HeroSectionUpdateDto dto, CancellationToken cancellationToken)
    {
        var success = await _adminService.UpdateHeroSectionAsync(dto, cancellationToken);
        return Ok(new { success, message = "Hero section updated successfully." });
    }

    // Philosophies
    [HttpGet("philosophies")]
    public async Task<IActionResult> GetPhilosophies(CancellationToken cancellationToken)
    {
        var cards = await _adminService.GetPhilosophyCardsAsync(cancellationToken);
        return Ok(cards);
    }

    [HttpPost("philosophies")]
    public async Task<IActionResult> CreatePhilosophy([FromBody] PhilosophyCardCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var created = await _adminService.CreatePhilosophyCardAsync(dto, cancellationToken);
        return Ok(created);
    }

    [HttpPut("philosophies/{id}")]
    public async Task<IActionResult> UpdatePhilosophy(Guid id, [FromBody] PhilosophyCardCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var success = await _adminService.UpdatePhilosophyCardAsync(id, dto, cancellationToken);
        if (!success) return NotFound(new { message = "Philosophy card not found." });
        return Ok(new { success, message = "Philosophy card updated." });
    }

    [HttpDelete("philosophies/{id}")]
    public async Task<IActionResult> DeletePhilosophy(Guid id, CancellationToken cancellationToken)
    {
        var success = await _adminService.DeletePhilosophyCardAsync(id, cancellationToken);
        if (!success) return NotFound(new { message = "Philosophy card not found." });
        return Ok(new { success, message = "Philosophy card deleted." });
    }

    // Disciplines
    [HttpGet("disciplines")]
    public async Task<IActionResult> GetDisciplines(CancellationToken cancellationToken)
    {
        var cards = await _adminService.GetDisciplineCardsAsync(cancellationToken);
        return Ok(cards);
    }

    [HttpPost("disciplines")]
    public async Task<IActionResult> CreateDiscipline([FromBody] DisciplineCardCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var created = await _adminService.CreateDisciplineCardAsync(dto, cancellationToken);
        return Ok(created);
    }

    [HttpPut("disciplines/{id}")]
    public async Task<IActionResult> UpdateDiscipline(Guid id, [FromBody] DisciplineCardCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var success = await _adminService.UpdateDisciplineCardAsync(id, dto, cancellationToken);
        if (!success) return NotFound(new { message = "Discipline card not found." });
        return Ok(new { success, message = "Discipline card updated." });
    }

    [HttpDelete("disciplines/{id}")]
    public async Task<IActionResult> DeleteDiscipline(Guid id, CancellationToken cancellationToken)
    {
        var success = await _adminService.DeleteDisciplineCardAsync(id, cancellationToken);
        if (!success) return NotFound(new { message = "Discipline card not found." });
        return Ok(new { success, message = "Discipline card deleted." });
    }
}
