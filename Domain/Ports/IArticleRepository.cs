using System.Collections.Generic;
using Domain.Models;

namespace Domain.Ports
{
    public interface IArticleRepository
    {
        bool ArticleExists(Article article);
        void AddArticle(Article article);
        ICollection<Article> GetAllArticles();
    }
}
