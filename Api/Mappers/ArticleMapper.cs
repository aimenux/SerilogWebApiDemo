using Api.Payloads;
using Domain.Models;

namespace Api.Mappers
{
    public class ArticleMapper : IArticleMapper
    {
        public Article Map(ArticleDto articleDto)
        {
            return new Article
            {
                Title = articleDto.Title,
                Author = articleDto.Author,
                PublicationDate = articleDto.PublicationDate
            };
        }

        public ArticleDto Map(Article article)
        {
            return new ArticleDto
            {
                Title = article.Title,
                Author = article.Author,
                PublicationDate = article.PublicationDate
            };
        }
    }
}