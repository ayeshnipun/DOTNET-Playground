namespace Playground.Domain.Entities.ddd.Banking.ValueObjects;

public record Role(string Title, Money MinimumSalary, Money MaximumSalary)
{
    public static Role Create(string title, Money minimumSalary, Money maximumSalary)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be null or empty.", nameof(title));

        if (minimumSalary.Currency != maximumSalary.Currency)
            throw new ArgumentException("Minimum and maximum salary must have the same currency.");

        if (minimumSalary.Amount > maximumSalary.Amount)
            throw new ArgumentException("Minimum salary cannot be greater than maximum salary.");

        return new Role(title, minimumSalary, maximumSalary);
    }

    public bool IsSalaryWithinRange(Role role, Money salary)
    {
        if (salary.Currency != role.MinimumSalary.Currency)
            throw new ArgumentException("Salary currency does not match role salary currency.");

        return salary.Amount >= role.MinimumSalary.Amount && salary.Amount <= role.MaximumSalary.Amount;
    }
}