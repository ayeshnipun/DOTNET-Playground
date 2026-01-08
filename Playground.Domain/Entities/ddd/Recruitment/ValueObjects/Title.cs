namespace Playground.Domain.Entities.ddd.Recruitment.ValueObjects;

public record Title
{
    public string Value { get; }

    public Title(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Title cannot be empty.", nameof(value));
        }

        if (value.Length > 100)
        {
            throw new ArgumentException("Title cannot exceed 100 characters.", nameof(value));
        }

        Value = value;
    }

    public override string ToString() => Value;
}