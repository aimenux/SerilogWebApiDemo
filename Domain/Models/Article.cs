using System;

namespace Domain.Models;

public sealed record Article
{
    public string Title { get; init; }
    public string Author { get; init; }
    public DateTime PublicationDate { get; init; }
}