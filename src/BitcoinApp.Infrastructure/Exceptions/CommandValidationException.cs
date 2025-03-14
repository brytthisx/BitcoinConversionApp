namespace BitcoinApp.Infrastructure.Exceptions;

public class CommandValidationException(string msg, Dictionary<string, string[]> content) : Exception(msg)
{
    public Dictionary<string, string[]> Content { get; } = content;
}
