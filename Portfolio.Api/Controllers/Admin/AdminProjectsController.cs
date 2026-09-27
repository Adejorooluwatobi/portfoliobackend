using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Admin;
using Portfolio.Application.Services;

namespace Portfolio.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/[controller]")]
[Authorize]
public class AdminProjectsController : ControllerBase
{
    private readonly IPortfolioAdminService _adminService;

    public AdminProjectsController(IPortfolioAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProjects(CancellationToken cancellationToken)
    {
        var list = await _adminService.GetProjectsAsync(cancellationToken);
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(Guid id, CancellationToken cancellationToken)
    {
        var project = await _adminService.GetProjectByIdAsync(id, cancellationToken);
        if (project == null) return NotFound(new { message = "Project not found." });
        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] ProjectCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var created = await _adminService.CreateProjectAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetProjectById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(Guid id, [FromBody] ProjectCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var success = await _adminService.UpdateProjectAsync(id, dto, cancellationToken);
        if (!success) return NotFound(new { message = "Project not found." });
        return Ok(new { success, message = "Project updated successfully." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(Guid id, CancellationToken cancellationToken)
    {
        var success = await _adminService.DeleteProjectAsync(id, cancellationToken);
        if (!success) return NotFound(new { message = "Project not found." });
        return Ok(new { success, message = "Project deleted successfully." });
    }

    // Case Study
    [HttpGet("{id}/case-study")]
    public async Task<IActionResult> GetCaseStudy(Guid id, CancellationToken cancellationToken)
    {
        var cs = await _adminService.GetCaseStudyAsync(id, cancellationToken);
        if (cs == null) return NotFound(new { message = "Case study not found for this project." });
        return Ok(cs);
    }

    [HttpPut("{id}/case-study")]
    public async Task<IActionResult> UpdateCaseStudy(Guid id, [FromBody] CaseStudyUpdateDto dto, CancellationToken cancellationToken)
    {
        var success = await _adminService.UpdateCaseStudyAsync(id, dto, cancellationToken);
        return Ok(new { success, message = "Case study updated successfully." });
    }

    // Project Categories
    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        var list = await _adminService.GetCategoriesAsync(cancellationToken);
        return Ok(list);
    }

    [HttpPost("categories")]
    public async Task<IActionResult> CreateCategory([FromBody] ProjectCategoryCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var created = await _adminService.CreateCategoryAsync(dto, cancellationToken);
        return Ok(created);
    }

    [HttpPut("categories/{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] ProjectCategoryCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var success = await _adminService.UpdateCategoryAsync(id, dto, cancellationToken);
        if (!success) return NotFound(new { message = "Category not found." });
        return Ok(new { success, message = "Category updated successfully." });
    }

    [HttpDelete("categories/{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        var success = await _adminService.DeleteCategoryAsync(id, cancellationToken);
        if (!success) return NotFound(new { message = "Category not found." });
        return Ok(new { success, message = "Category deleted successfully." });
    }
}
