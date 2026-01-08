namespace Playground.Domain.Entities.ddd.Banking.ValueObjects;

public record Email(string Address)
{
    public static Email Create(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Email address cannot be null or empty.", nameof(address));

        if (!address.Contains("@") || !address.Contains("."))
            throw new ArgumentException("Email address is not valid.", nameof(address));

        return new Email(address);
    }
}