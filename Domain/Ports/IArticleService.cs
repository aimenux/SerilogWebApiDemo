using System;
using System.Collections.Generic;
using Domain.Models;

namespace Domain.Ports
{
    public interface IArticleService
    {
        void AddArticle(Article article);
        ICollection<Article> GetArticlesForAuthor(string author);
        ICollection<Article> GetArticlesPublishedAfterDate(DateTime publicationDate);
    }
}
