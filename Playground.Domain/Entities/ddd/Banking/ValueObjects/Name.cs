namespace Playground.Domain.Entities.ddd.Banking.ValueObjects;

public record Name(string FirstName, string LastName)
{
    public static Name Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));

        return new Name(firstName, lastName);
    }
}