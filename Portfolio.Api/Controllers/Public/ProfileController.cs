using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Public;
using Portfolio.Application.Services;

namespace Portfolio.Api.Controllers.Public;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class ProfileController : ControllerBase
{
    private readonly IPortfolioPublicService _publicService;

    public ProfileController(IPortfolioPublicService publicService)
    {
        _publicService = publicService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ProfileResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        var profile = await _publicService.GetProfileAsync(cancellationToken);
        if (profile == null) return NotFound(new { message = "Profile not found." });

        return Ok(profile);
    }

    [HttpGet("hero")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHero(CancellationToken cancellationToken)
    {
        var profile = await _publicService.GetProfileAsync(cancellationToken);
        if (profile?.HeroSection == null) return NotFound(new { message = "Hero section not found." });

        return Ok(profile.HeroSection);
    }
}
