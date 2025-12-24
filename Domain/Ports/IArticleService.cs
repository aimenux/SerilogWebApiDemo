using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Ports;

public interface IArticleService
{
    Task AddArticleAsync(Article article, CancellationToken cancellationToken);
    
    Task<ICollection<Article>> GetArticlesAsync(CancellationToken cancellationToken);
    
    Task<ICollection<Article>> GetArticlesForAuthorAsync(string author, CancellationToken cancellationToken);
    
    Task<ICollection<Article>> GetArticlesPublishedAfterDateAsync(DateTime publicationDate, CancellationToken cancellationToken);
}