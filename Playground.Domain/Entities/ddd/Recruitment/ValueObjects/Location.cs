using Playground.Domain.Entities.ddd.Recruitment.Enums;

namespace Playground.Domain.Entities.ddd.Recruitment.ValueObjects;

public record Location
{
    public LocationType LocationType { get; }

    public Location(LocationType value)
    {
        if (!Enum.IsDefined(typeof(LocationType), value))
            throw new ArgumentException("Invalid location type.", nameof(value));

        LocationType = value;
    }

    public override string ToString() => LocationType.ToString();
}