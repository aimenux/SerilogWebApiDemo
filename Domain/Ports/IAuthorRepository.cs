namespace Domain.Ports;

public interface IAuthorRepository
{
    void AddToBlackListAuthors(string author);
    bool IsBlackListedAuthor(string author);
}