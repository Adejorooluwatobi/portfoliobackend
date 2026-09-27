using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Admin;
using Portfolio.Application.Services;

namespace Portfolio.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/[controller]")]
[Authorize]
public class AdminResumeController : ControllerBase
{
    private readonly IPortfolioAdminService _adminService;

    public AdminResumeController(IPortfolioAdminService adminService)
    {
        _adminService = adminService;
    }

    // Experiences
    [HttpGet("experiences")]
    public async Task<IActionResult> GetExperiences(CancellationToken cancellationToken)
    {
        var list = await _adminService.GetExperiencesAsync(cancellationToken);
        return Ok(list);
    }

    [HttpPost("experiences")]
    public async Task<IActionResult> CreateExperience([FromBody] WorkExperienceCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var created = await _adminService.CreateExperienceAsync(dto, cancellationToken);
        return Ok(created);
    }

    [HttpPut("experiences/{id}")]
    public async Task<IActionResult> UpdateExperience(Guid id, [FromBody] WorkExperienceCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var success = await _adminService.UpdateExperienceAsync(id, dto, cancellationToken);
        if (!success) return NotFound(new { message = "Experience entry not found." });
        return Ok(new { success, message = "Experience updated successfully." });
    }

    [HttpDelete("experiences/{id}")]
    public async Task<IActionResult> DeleteExperience(Guid id, CancellationToken cancellationToken)
    {
        var success = await _adminService.DeleteExperienceAsync(id, cancellationToken);
        if (!success) return NotFound(new { message = "Experience entry not found." });
        return Ok(new { success, message = "Experience deleted successfully." });
    }

    // Educations
    [HttpGet("educations")]
    public async Task<IActionResult> GetEducations(CancellationToken cancellationToken)
    {
        var list = await _adminService.GetEducationsAsync(cancellationToken);
        return Ok(list);
    }

    [HttpPost("educations")]
    public async Task<IActionResult> CreateEducation([FromBody] EducationCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var created = await _adminService.CreateEducationAsync(dto, cancellationToken);
        return Ok(created);
    }

    [HttpPut("educations/{id}")]
    public async Task<IActionResult> UpdateEducation(Guid id, [FromBody] EducationCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var success = await _adminService.UpdateEducationAsync(id, dto, cancellationToken);
        if (!success) return NotFound(new { message = "Education entry not found." });
        return Ok(new { success, message = "Education updated successfully." });
    }

    [HttpDelete("educations/{id}")]
    public async Task<IActionResult> DeleteEducation(Guid id, CancellationToken cancellationToken)
    {
        var success = await _adminService.DeleteEducationAsync(id, cancellationToken);
        if (!success) return NotFound(new { message = "Education entry not found." });
        return Ok(new { success, message = "Education deleted successfully." });
    }

    // Skill Categories
    [HttpGet("skill-categories")]
    public async Task<IActionResult> GetSkillCategories(CancellationToken cancellationToken)
    {
        var list = await _adminService.GetSkillCategoriesAsync(cancellationToken);
        return Ok(list);
    }

    [HttpPost("skill-categories")]
    public async Task<IActionResult> CreateSkillCategory([FromBody] SkillCategoryCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var created = await _adminService.CreateSkillCategoryAsync(dto, cancellationToken);
        return Ok(created);
    }

    [HttpPut("skill-categories/{id}")]
    public async Task<IActionResult> UpdateSkillCategory(Guid id, [FromBody] SkillCategoryCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var success = await _adminService.UpdateSkillCategoryAsync(id, dto, cancellationToken);
        if (!success) return NotFound(new { message = "Skill category not found." });
        return Ok(new { success, message = "Skill category updated." });
    }

    [HttpDelete("skill-categories/{id}")]
    public async Task<IActionResult> DeleteSkillCategory(Guid id, CancellationToken cancellationToken)
    {
        var success = await _adminService.DeleteSkillCategoryAsync(id, cancellationToken);
        if (!success) return NotFound(new { message = "Skill category not found." });
        return Ok(new { success, message = "Skill category deleted." });
    }

    // Skill Items
    [HttpPost("skills")]
    public async Task<IActionResult> CreateSkillItem([FromBody] SkillItemCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var created = await _adminService.CreateSkillItemAsync(dto, cancellationToken);
        return Ok(created);
    }

    [HttpPut("skills/{id}")]
    public async Task<IActionResult> UpdateSkillItem(Guid id, [FromBody] SkillItemCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var success = await _adminService.UpdateSkillItemAsync(id, dto, cancellationToken);
        if (!success) return NotFound(new { message = "Skill item not found." });
        return Ok(new { success, message = "Skill item updated." });
    }

    [HttpDelete("skills/{id}")]
    public async Task<IActionResult> DeleteSkillItem(Guid id, CancellationToken cancellationToken)
    {
        var success = await _adminService.DeleteSkillItemAsync(id, cancellationToken);
        if (!success) return NotFound(new { message = "Skill item not found." });
        return Ok(new { success, message = "Skill item deleted." });
    }
}
