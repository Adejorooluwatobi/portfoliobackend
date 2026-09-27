using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Infrastructure.Data;

namespace Portfolio.Api.Controllers.Public;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class HealthController : ControllerBase
{
    private readonly AppDbContext _context;

    public HealthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetHealth(CancellationToken cancellationToken)
    {
        bool databaseConnected = false;
        string? dbError = null;

        try
        {
            databaseConnected = await _context.Database.CanConnectAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            dbError = ex.Message;
        }

        return Ok(new
        {
            status = "Healthy",
            service = "Oluwatobi Portfolio API",
            architecture = "Clean Architecture (ASP.NET Core 10 / .NET 10)",
            database = databaseConnected ? "Connected" : "Disconnected",
            databaseError = dbError,
            timestamp = DateTime.UtcNow
        });
    }
}
