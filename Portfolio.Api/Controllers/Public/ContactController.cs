using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Public;
using Portfolio.Application.Services;

namespace Portfolio.Api.Controllers.Public;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class ContactController : ControllerBase
{
    private readonly IPortfolioPublicService _publicService;

    public ContactController(IPortfolioPublicService publicService)
    {
        _publicService = publicService;
    }

    [HttpGet("info")]
    [ProducesResponseType(typeof(ContactInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetContactInfo(CancellationToken cancellationToken)
    {
        var info = await _publicService.GetContactInfoAsync(cancellationToken);
        if (info == null) return NotFound(new { message = "Contact information not found." });

        return Ok(info);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateInquiryResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CreateInquiryResponseDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SubmitInquiry([FromBody] CreateInquiryRequestDto request, CancellationToken cancellationToken)
    {
        var response = await _publicService.SubmitContactInquiryAsync(request, cancellationToken);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}
