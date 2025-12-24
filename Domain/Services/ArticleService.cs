using Domain.Exceptions;
using Domain.Extensions;
using Domain.Models;
using Domain.Ports;

namespace Domain.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IAuthorRepository _authorRepository;

    public ArticleService(IArticleRepository articleRepository, IAuthorRepository authorRepository)
    {
        _articleRepository = articleRepository;
        _authorRepository = authorRepository;
    }

    public Task AddArticleAsync(Article article, CancellationToken cancellationToken)
    {
        if (article == null)
        {
            return Task.CompletedTask;
        }

        var customProperties = new Dictionary<string, object>
        {
            [nameof(article.Title)] = article.Title,
            [nameof(article.Author)] = article.Author,
            [nameof(article.PublicationDate)] = article.PublicationDate
        };

        if (_authorRepository.IsBlackListedAuthor(article.Author))
        {
            throw BusinessValidationException.AuthorIsBlackListed(article.Author, customProperties);
        }

        if (_articleRepository.ArticleExists(article))
        {
            throw BusinessException.ArticleExists(article.Title, customProperties);
        }

        return _articleRepository.AddArticleAsync(article, cancellationToken);
    }
    
    public Task<ICollection<Article>> GetArticlesAsync(CancellationToken cancellationToken)
    {
        return _articleRepository.GetArticlesAsync(cancellationToken);
    }

    public Task<ICollection<Article>> GetArticlesForAuthorAsync(string author, CancellationToken cancellationToken)
    {
        return _articleRepository.GetArticlesAsync(x => x.Author.EqualsIgnoreCase(author), cancellationToken);
    }

    public Task<ICollection<Article>> GetArticlesPublishedAfterDateAsync(DateTime publicationDate, CancellationToken cancellationToken)
    {
        return _articleRepository.GetArticlesAsync(x => x.PublicationDate >= publicationDate, cancellationToken);
    }
}