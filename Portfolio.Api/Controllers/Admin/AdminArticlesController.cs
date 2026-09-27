using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Admin;
using Portfolio.Application.Services;

namespace Portfolio.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/[controller]")]
[Authorize]
public class AdminArticlesController : ControllerBase
{
    private readonly IPortfolioAdminService _adminService;

    public AdminArticlesController(IPortfolioAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet]
    public async Task<IActionResult> GetArticles(CancellationToken cancellationToken)
    {
        var list = await _adminService.GetArticlesAsync(cancellationToken);
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetArticleById(Guid id, CancellationToken cancellationToken)
    {
        var article = await _adminService.GetArticleByIdAsync(id, cancellationToken);
        if (article == null) return NotFound(new { message = "Article not found." });
        return Ok(article);
    }

    [HttpPost]
    public async Task<IActionResult> CreateArticle([FromBody] ArticleCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var created = await _adminService.CreateArticleAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetArticleById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateArticle(Guid id, [FromBody] ArticleCreateUpdateDto dto, CancellationToken cancellationToken)
    {
        var success = await _adminService.UpdateArticleAsync(id, dto, cancellationToken);
        if (!success) return NotFound(new { message = "Article not found." });
        return Ok(new { success, message = "Article updated successfully." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArticle(Guid id, CancellationToken cancellationToken)
    {
        var success = await _adminService.DeleteArticleAsync(id, cancellationToken);
        if (!success) return NotFound(new { message = "Article not found." });
        return Ok(new { success, message = "Article deleted successfully." });
    }
}
