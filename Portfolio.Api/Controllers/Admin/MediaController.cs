using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.Common.Interfaces;

namespace Portfolio.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/[controller]")]
[Authorize]
public class MediaController : ControllerBase
{
    private readonly ICloudinaryService _cloudinaryService;
    private readonly ILogger<MediaController> _logger;

    public MediaController(ICloudinaryService cloudinaryService, ILogger<MediaController> logger)
    {
        _cloudinaryService = cloudinaryService;
        _logger = logger;
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromQuery] string folder = "portfolio", CancellationToken cancellationToken = default)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new { message = "No file was uploaded." });
        }

        // Validate max size (10MB)
        if (file.Length > 10 * 1024 * 1024)
        {
            return BadRequest(new { message = "File size exceeds the 10MB limit." });
        }

        await using var stream = file.OpenReadStream();
        var result = await _cloudinaryService.UploadImageAsync(stream, file.FileName, folder, cancellationToken);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("upload-doc")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadDocument([FromForm] IFormFile file, [FromQuery] string folder = "portfolio/docs", CancellationToken cancellationToken = default)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new { message = "No document file was uploaded." });
        }

        if (file.Length > 25 * 1024 * 1024)
        {
            return BadRequest(new { message = "Document size exceeds the 25MB limit." });
        }

        await using var stream = file.OpenReadStream();
        var result = await _cloudinaryService.UploadRawFileAsync(stream, file.FileName, folder, cancellationToken);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpDelete("{publicId}")]
    public async Task<IActionResult> DeleteMedia(string publicId, CancellationToken cancellationToken)
    {
        var success = await _cloudinaryService.DeleteMediaAsync(publicId, cancellationToken);
        if (!success)
        {
            return BadRequest(new { message = $"Could not delete media with id '{publicId}'" });
        }

        return Ok(new { success = true, message = "Media deleted successfully." });
    }
}
