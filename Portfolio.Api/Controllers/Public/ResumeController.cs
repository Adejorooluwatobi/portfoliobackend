using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Public;
using Portfolio.Application.Services;

namespace Portfolio.Api.Controllers.Public;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class ResumeController : ControllerBase
{
    private readonly IPortfolioPublicService _publicService;

    public ResumeController(IPortfolioPublicService publicService)
    {
        _publicService = publicService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResumeResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetResume(CancellationToken cancellationToken)
    {
        var resume = await _publicService.GetResumeAsync(cancellationToken);
        if (resume == null) return NotFound(new { message = "Resume data not found." });

        return Ok(resume);
    }
}
