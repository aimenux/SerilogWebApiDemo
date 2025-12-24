namespace Domain.Exceptions;

public class BusinessException : ApplicationException
{
    protected BusinessException()
    {
    }

    protected BusinessException(string message) : base(message)
    {
    }

    protected BusinessException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public static BusinessException ArticleExists(string title, IDictionary<string, object> customProperties = null)
    {
        var exception = new BusinessException($"Article {title} is already registered.");

        AddCustomProperties(exception, customProperties);

        return exception;
    }

    protected static void AddCustomProperties(Exception exception, IDictionary<string, object> customProperties)
    {
        if (customProperties == null)
        {
            return;
        }

        foreach (var item in customProperties)
        {
            exception.Data.Add(item.Key, item.Value);
        }
    }
}