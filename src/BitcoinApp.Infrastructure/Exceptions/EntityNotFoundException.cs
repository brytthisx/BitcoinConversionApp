namespace BitcoinApp.Infrastructure.Exceptions;

public class EntityNotFoundException(string message) : InfrastructureException(message);
