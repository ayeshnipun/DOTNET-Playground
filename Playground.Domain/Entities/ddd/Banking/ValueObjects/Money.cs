namespace Playground.Domain.Entities.ddd.Banking.ValueObjects;

public record Money(decimal Amount, string Currency)
{
    public static Money Create(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative.", nameof(amount));

        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency cannot be null or empty.", nameof(currency));

        return new Money(amount, currency);
    }

    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new ArgumentException("Cannot add money with different currencies.");

        return new Money(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        if (Currency != other.Currency)
            throw new ArgumentException("Cannot subtract money with different currencies.");

        if (Amount < other.Amount)
            throw new InvalidOperationException("Resulting amount cannot be negative.");

        return new Money(Amount - other.Amount, Currency);
    }

    public override string ToString() => $"{Amount} {Currency}";
}