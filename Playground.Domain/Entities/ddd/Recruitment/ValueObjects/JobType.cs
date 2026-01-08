using Playground.Domain.Entities.ddd.Recruitment.Enums;

namespace Playground.Domain.Entities.ddd.Recruitment.ValueObjects;

public record JobType
{
    public JobPosition JobPosition { get; }

    public JobType(JobPosition value)
    {
        if (!Enum.IsDefined(typeof(JobPosition), value))
            throw new ArgumentException("Invalid job position type.", nameof(value));

        JobPosition = value;
    }

    public override string ToString() => JobPosition.ToString();
}