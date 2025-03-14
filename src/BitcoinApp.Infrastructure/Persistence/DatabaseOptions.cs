using System.ComponentModel.DataAnnotations;


namespace BitcoinApp.Infrastructure.Persistence;

public class DatabaseOptions : IValidatableObject
{
    public string ConnectionString { get; set; } = string.Empty;


    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(ConnectionString))
        {
            yield return new ValidationResult("connection string can't be empty.", new[] { nameof(ConnectionString) });
        }
    }
}
