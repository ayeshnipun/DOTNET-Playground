namespace Playground.Domain.Entities.ddd.Recruitment.ValueObjects;

public record Skill
{
    public string Value { get; }

    public Skill(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Skill cannot be empty.", nameof(value));
        }

        if (value.Length > 50)
        {
            throw new ArgumentException("Skill cannot exceed 50 characters.", nameof(value));
        }

        Value = value;
    }

    public override string ToString() => Value;
}