using System.Collections.Generic;
using System.Linq;
using Api.Mappers;
using Api.Payloads;
using Domain.Ports;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleMapper _articleMapper;
        private readonly IArticleService _articleService;

        public ArticleController(IArticleMapper articleMapper, IArticleService articleService)
        {
            _articleMapper = articleMapper;
            _articleService = articleService;
        }

        [HttpPost("create")]
        public void CreateArticle(ArticleDto articlesDto)
        {
            var article = _articleMapper.Map(articlesDto);
            _articleService.AddArticle(article);
        }

        [HttpPost("search")]
        public ICollection<ArticleDto> SearchArticles(string author)
        {
            var articles = _articleService.GetArticlesForAuthor(author);
            var results = articles
                .Select(x => _articleMapper.Map(x))
                .ToList();
            return results;
        }
    }
}
