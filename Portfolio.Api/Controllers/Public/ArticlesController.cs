using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Public;
using Portfolio.Application.Services;

namespace Portfolio.Api.Controllers.Public;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class ArticlesController : ControllerBase
{
    private readonly IPortfolioPublicService _publicService;

    public ArticlesController(IPortfolioPublicService publicService)
    {
        _publicService = publicService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ArticleDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetArticles(CancellationToken cancellationToken)
    {
        var articles = await _publicService.GetArticlesAsync(cancellationToken);
        return Ok(articles);
    }
}
