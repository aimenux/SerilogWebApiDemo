using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Ports;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly IMemoryCache _cache;
        private readonly HashSet<string> _keys;

        public ArticleRepository(IMemoryCache cache)
        {
            _cache = cache;
            _keys = new HashSet<string>();
        }

        public bool ArticleExists(Article article)
        {
            var key = BuildKey(article);
            return _cache.TryGetValue(key, out var _);
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

        public ICollection<Article> GetAllArticles()
        {
            Article GetArticle(string key)
            {
                return _cache.TryGetValue(key, out Article article) ? article : null;
            }

            return _keys.Select(GetArticle).ToList();
        }

        private static string BuildKey(Article article)
        {
            return $"{article.Author}_{article.Title}".Trim().ToUpper();
        }
    }
}
