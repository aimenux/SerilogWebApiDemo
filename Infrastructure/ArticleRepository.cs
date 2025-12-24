using Domain.Models;
using Domain.Ports;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure;

public sealed class ArticleRepository : IArticleRepository
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

    public async Task AddArticleAsync(Article article, CancellationToken cancellationToken)
    {
        if (ArticleExists(article))
        {
            return;
        }
        
        await RandomDelayAsync(cancellationToken);

        var key = BuildKey(article);
        _keys.Add(key);
        _cache.Set(key, article);
    }

    public async Task<ICollection<Article>> GetArticlesAsync(Predicate<Article> filter, CancellationToken cancellationToken)
    {
        await RandomDelayAsync(cancellationToken);
        
        return _keys
            .Where(x => filter is null || filter(GetArticle(x)))
            .Select(GetArticle)
            .Where(x => x != null)
            .ToList();
    }

    private Article GetArticle(string key)
    {
        return _cache.TryGetValue(key, out Article article) 
            ? article 
            : null;
    }

    private static string BuildKey(Article article)
    {
        return $"{article.Author}_{article.Title}".Trim().ToUpper(); 
    }

    private static Task RandomDelayAsync(CancellationToken cancellationToken)
    {
        return Task.Delay(Random.Shared.Next(10, 1000), cancellationToken);
    }
}