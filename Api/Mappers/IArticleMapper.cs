using Api.Payloads;
using Domain.Models;

namespace Api.Mappers;

public interface IArticleMapper
{
    Article Map(ArticleDto articleDto);
    
    ArticleDto Map(Article articleDto);
}