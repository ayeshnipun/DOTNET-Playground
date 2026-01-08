namespace Playground.Domain.Entities.ddd.Recruitment.ValueObjects;

public record Description
{
    public string Value { get; }

    public Description(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Description cannot be empty.", nameof(value));
        }

        if (value.Length > 1000)
        {
            throw new ArgumentException("Description cannot exceed 1000 characters.", nameof(value));
        }

        Value = value;
    }

    public override string ToString() => Value;
}