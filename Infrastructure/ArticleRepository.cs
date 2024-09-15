using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Ports;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure;

public class ArticleRepository : IArticleRepository
{
    private readonly IMemoryCache _cache;
    private readonly HashSet<string> _keys;

    public ArticleRepository(IMemoryCache cache)
    {
        _cache = cache;
        _keys = [];
    }

    public bool ArticleExists(Article article)
    {
        var key = BuildKey(article);
        return _cache.TryGetValue(key, out _);
    }

    public void AddArticle(Article article)
    {
        if (ArticleExists(article))
        {
            return;
        }

        var key = BuildKey(article);
        _keys.Add(key);
        _cache.Set(key, article);
    }

    public ICollection<Article> GetAllArticles() => _keys.Select(GetArticle).ToList();
    
    private Article GetArticle(string key) => _cache.TryGetValue(key, out Article article) ? article : null;

    private static string BuildKey(Article article)
    {
        return $"{article.Author}_{article.Title}".Trim().ToUpper(); 
    }
}