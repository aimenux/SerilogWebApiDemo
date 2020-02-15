using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;
using Domain.Models;
using Domain.Ports;

namespace Domain.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IAuthorRepository _authorRepository;

        public ArticleService(IArticleRepository articleRepository, IAuthorRepository authorRepository)
        {
            _articleRepository = articleRepository;
            _authorRepository = authorRepository;
        }

        public void AddArticle(Article article)
        {
            if (article == null)
            {
                return;
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

            _articleRepository.AddArticle(article);
        }

        public ICollection<Article> GetArticlesForAuthor(string author)
        {
            return _articleRepository.GetAllArticles().Where(article => string.Equals(article.Author, author, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public ICollection<Article> GetArticlesPublishedAfterDate(DateTime publicationDate)
        {
            return _articleRepository.GetAllArticles().Where(article => article.PublicationDate >= publicationDate).ToList();
        }
    }
}
