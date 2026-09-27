using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Admin;
using Portfolio.Application.Services;

namespace Portfolio.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/[controller]")]
[Authorize]
public class AdminSettingsController : ControllerBase
{
    private readonly IPortfolioAdminService _adminService;

    public AdminSettingsController(IPortfolioAdminService adminService)
    {
        _adminService = adminService;
    }

    // Site Settings
    [HttpGet]
    public async Task<IActionResult> GetSettings(CancellationToken cancellationToken)
    {
        var settings = await _adminService.GetSiteSettingsAsync(cancellationToken);
        return Ok(settings);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSettings([FromBody] SiteSettingsUpdateDto dto, CancellationToken cancellationToken)
    {
        var success = await _adminService.UpdateSiteSettingsAsync(dto, cancellationToken);
        return Ok(new { success, message = "Site settings updated successfully." });
    }

    // Page Settings
    [HttpGet("pages")]
    public async Task<IActionResult> GetPageSettings(CancellationToken cancellationToken)
    {
        var list = await _adminService.GetPageSettingsAsync(cancellationToken);
        return Ok(list);
    }

    [HttpPut("pages/{key}")]
    public async Task<IActionResult> UpdatePageSettings(string key, [FromBody] PageSettingsUpdateDto dto, CancellationToken cancellationToken)
    {
        var success = await _adminService.UpdatePageSettingsAsync(key, dto, cancellationToken);
        return Ok(new { success, message = $"Page settings for '{key}' updated successfully." });
    }

    // Navigation
    [HttpGet("nav")]
    public async Task<IActionResult> GetNavItems(CancellationToken cancellationToken)
    {
        var list = await _adminService.GetNavItemsAsync(cancellationToken);
        return Ok(list);
    }

    [HttpPost("nav")]
    public async Task<IActionResult> CreateNavItem([FromBody] NavItemCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var created = await _adminService.CreateNavItemAsync(dto, cancellationToken);
        return Ok(created);
    }

    [HttpPut("nav/{id}")]
    public async Task<IActionResult> UpdateNavItem(Guid id, [FromBody] NavItemCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var success = await _adminService.UpdateNavItemAsync(id, dto, cancellationToken);
        if (!success) return NotFound(new { message = "Nav item not found." });
        return Ok(new { success, message = "Nav item updated successfully." });
    }

    [HttpDelete("nav/{id}")]
    public async Task<IActionResult> DeleteNavItem(Guid id, CancellationToken cancellationToken)
    {
        var success = await _adminService.DeleteNavItemAsync(id, cancellationToken);
        if (!success) return NotFound(new { message = "Nav item not found." });
        return Ok(new { success, message = "Nav item deleted successfully." });
    }
}
