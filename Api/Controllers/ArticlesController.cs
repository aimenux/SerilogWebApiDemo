using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Api.Mappers;
using Api.Payloads;
using Domain.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticlesController : ControllerBase
{
    private readonly IArticleMapper _articleMapper;
    private readonly IArticleService _articleService;

    public ArticlesController(IArticleMapper articleMapper, IArticleService articleService)
    {
        _articleMapper = articleMapper;
        _articleService = articleService;
    }

    [HttpPost]
    public async Task CreateArticle(ArticleDto articlesDto, CancellationToken cancellationToken)
    {
        var article = _articleMapper.Map(articlesDto);
        await _articleService.AddArticleAsync(article, cancellationToken);
    }
    
    [HttpGet]
    public async Task<ICollection<ArticleDto>> GetArticles(CancellationToken cancellationToken)
    {
        var articles = await _articleService.GetArticlesAsync(cancellationToken);
        var results = articles
            .Select(x => _articleMapper.Map(x))
            .ToList();
        return results;
    }

    [HttpGet("search/{author}")]
    public async Task<ICollection<ArticleDto>> SearchArticles(string author, CancellationToken cancellationToken)
    {
        var articles = await _articleService.GetArticlesForAuthorAsync(author, cancellationToken);
        var results = articles
            .Select(x => _articleMapper.Map(x))
            .ToList();
        return results;
    }
}