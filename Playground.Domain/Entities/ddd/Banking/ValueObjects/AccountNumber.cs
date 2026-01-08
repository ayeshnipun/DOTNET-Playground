namespace Playground.Domain.Entities.ddd.Banking.ValueObjects;

public record AccountNumber(string Number)
{
    public static AccountNumber Create(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Account number cannot be null or empty.", nameof(number));

        if (number.Length != 12)
            throw new ArgumentException("Account number must be exactly 12 characters long.", nameof(number));

        if (!number.All(char.IsDigit))
            throw new ArgumentException("Account number must contain only digits.", nameof(number));

        return new AccountNumber(number);
    }
}