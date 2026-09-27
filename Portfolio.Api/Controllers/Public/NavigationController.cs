using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Public;
using Portfolio.Application.Services;

namespace Portfolio.Api.Controllers.Public;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class NavigationController : ControllerBase
{
    private readonly IPortfolioPublicService _publicService;

    public NavigationController(IPortfolioPublicService publicService)
    {
        _publicService = publicService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(NavigationResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNavigation(CancellationToken cancellationToken)
    {
        var nav = await _publicService.GetNavigationAsync(cancellationToken);
        return Ok(nav);
    }

    [HttpGet("page/{key}")]
    [ProducesResponseType(typeof(PageSettingsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPageSettings(string key, CancellationToken cancellationToken)
    {
        var settings = await _publicService.GetPageSettingsAsync(key, cancellationToken);
        if (settings == null) return NotFound(new { message = $"Page settings for '{key}' not found." });

        return Ok(settings);
    }
}
