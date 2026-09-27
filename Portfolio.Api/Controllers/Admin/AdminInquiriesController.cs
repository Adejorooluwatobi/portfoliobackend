using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Admin;
using Portfolio.Application.Services;

namespace Portfolio.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/[controller]")]
[Authorize]
public class AdminInquiriesController : ControllerBase
{
    private readonly IPortfolioAdminService _adminService;

    public AdminInquiriesController(IPortfolioAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet]
    public async Task<IActionResult> GetInquiries([FromQuery] string? filter, CancellationToken cancellationToken)
    {
        var inquiries = await _adminService.GetInquiriesAsync(filter, cancellationToken);
        return Ok(inquiries);
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats(CancellationToken cancellationToken)
    {
        var stats = await _adminService.GetInquiryStatsAsync(cancellationToken);
        return Ok(stats);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInquiryById(Guid id, CancellationToken cancellationToken)
    {
        var inquiry = await _adminService.GetInquiryByIdAsync(id, cancellationToken);
        if (inquiry == null) return NotFound(new { message = "Inquiry not found." });
        return Ok(inquiry);
    }

    [HttpPut("{id}/read")]
    public async Task<IActionResult> ToggleRead(Guid id, CancellationToken cancellationToken)
    {
        var success = await _adminService.ToggleInquiryReadAsync(id, cancellationToken);
        if (!success) return NotFound(new { message = "Inquiry not found." });
        return Ok(new { success, message = "Inquiry read status updated." });
    }

    [HttpPut("{id}/archive")]
    public async Task<IActionResult> ToggleArchive(Guid id, CancellationToken cancellationToken)
    {
        var success = await _adminService.ToggleInquiryArchivedAsync(id, cancellationToken);
        if (!success) return NotFound(new { message = "Inquiry not found." });
        return Ok(new { success, message = "Inquiry archive status updated." });
    }

    [HttpPut("{id}/notes")]
    public async Task<IActionResult> UpdateNotes(Guid id, [FromBody] UpdateInquiryNotesDto dto, CancellationToken cancellationToken)
    {
        var success = await _adminService.UpdateInquiryNotesAsync(id, dto.Notes, cancellationToken);
        if (!success) return NotFound(new { message = "Inquiry not found." });
        return Ok(new { success, message = "Admin notes saved successfully." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInquiry(Guid id, CancellationToken cancellationToken)
    {
        var success = await _adminService.DeleteInquiryAsync(id, cancellationToken);
        if (!success) return NotFound(new { message = "Inquiry not found." });
        return Ok(new { success, message = "Inquiry deleted successfully." });
    }
}
