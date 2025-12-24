namespace Domain.Exceptions;

public class BusinessValidationException : BusinessException
{
    protected BusinessValidationException()
    {
    }

    protected BusinessValidationException(string message) : base(message)
    {
    }

    protected BusinessValidationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public static BusinessValidationException AuthorIsBlackListed(string author, IDictionary<string, object> customProperties = null)
    {
        var exception = new BusinessValidationException($"Author {author} is blacklisted.");

        AddCustomProperties(exception, customProperties);

        return exception;
    }
}