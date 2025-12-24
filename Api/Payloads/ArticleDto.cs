using System.ComponentModel.DataAnnotations;

namespace Api.Payloads;

public sealed record ArticleDto
{
    [Required]
    public string Title { get; init; }
        
    [Required]
    public string Author { get; init; }

    [Required]
    public DateTime PublicationDate { get; init; }
}