namespace Playground.Domain.Entities.ddd.Gym;

public sealed class DateRange
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    public int Days => (EndDate - StartDate).Days;

    private DateRange(DateTime startDate, DateTime endDate)
    {
        if (endDate <= startDate)
            throw new ArgumentException("End date must be after start date.");

        StartDate = startDate;
        EndDate = endDate;
    }

    public bool Overlaps(DateRange other)
        => StartDate < other.EndDate && EndDate > other.StartDate;

    public bool Equals(DateRange other)
        => StartDate == other.StartDate && EndDate == other.EndDate;

    public override int GetHashCode()
        => HashCode.Combine(StartDate, EndDate);

    public override bool Equals(object? obj)
        => obj is DateRange other && Equals(other);

    public static DateRange Create(DateTime startDate, DateTime endDate)
    {
        if (endDate <= startDate)
        {
            return null!;
        }

        return new DateRange(startDate, endDate);
    }
}