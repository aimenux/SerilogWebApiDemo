using Domain.Models;

namespace Domain.Ports;

public interface IArticleRepository
{
    bool ArticleExists(Article article);
    
    Task AddArticleAsync(Article article, CancellationToken cancellationToken);
    
    Task<ICollection<Article>> GetArticlesAsync(CancellationToken cancellationToken) => GetArticlesAsync(null, cancellationToken);
    
    Task<ICollection<Article>> GetArticlesAsync(Predicate<Article> filter, CancellationToken cancellationToken);
}