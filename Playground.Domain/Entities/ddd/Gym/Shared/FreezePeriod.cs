namespace Playground.Domain.Entities.ddd.Gym;

public sealed class FreezePeriod
{
    public DateRange Period { get; }

    public int Days => Period.Days;
    public int Year => Period.StartDate.Year;

    private FreezePeriod(DateRange period)
    {
        if (period.Days <= 0)
            throw new ArgumentException("Period must have a positive number of days.");

        Period = period;
    }

    public bool Equals(FreezePeriod other)
        => Period.Equals(other.Period);

    public static FreezePeriod Create(DateTime startDate, DateTime endDate)
    {
        if (endDate <= startDate)
        {
            return null!;
        }

        return new FreezePeriod(DateRange.Create(startDate, endDate));
    }
}