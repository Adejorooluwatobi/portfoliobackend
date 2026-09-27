using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Public;
using Portfolio.Application.Services;

namespace Portfolio.Api.Controllers.Public;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class ProjectsController : ControllerBase
{
    private readonly IPortfolioPublicService _publicService;

    public ProjectsController(IPortfolioPublicService publicService)
    {
        _publicService = publicService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ProjectsResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProjects([FromQuery] string? category, CancellationToken cancellationToken)
    {
        var result = await _publicService.GetProjectsAsync(category, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{slug}")]
    [ProducesResponseType(typeof(ProjectDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProjectBySlug(string slug, CancellationToken cancellationToken)
    {
        var project = await _publicService.GetProjectBySlugAsync(slug, cancellationToken);
        if (project == null) return NotFound(new { message = $"Project '{slug}' not found." });

        return Ok(project);
    }
}
