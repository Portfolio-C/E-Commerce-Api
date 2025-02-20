namespace ECommerce.Domain.Exceptions;

public class SecurityTokenException : Exception
{
    public SecurityTokenException() : base() { }
    public SecurityTokenException(string message) : base(message) { }
    public SecurityTokenException(string message, Exception innerException) : base(message, innerException) { }
}
