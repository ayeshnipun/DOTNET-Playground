using Playground.Domain.Entities.ddd.Banking.ValueObjects;

namespace Playground.Domain.Entities.ddd.Banking.AggregateRoots;

public class Employee
{
    public Guid Id { get; private set; }
    public Name FullName { get; private set; }
    public Role Role { get; private set; }
    public Address Address { get; private set; }
    public Money Salary { get; private set; }

    private Employee(Name fullName, Role role, Address address, Money salary)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        Role = role;
        Address = address;
        Salary = salary;
    }

    public static Employee Hire(Name fullName, Role role, Address address, Money startingSalary)
    {
        if (startingSalary.Amount < 30000)
            throw new ArgumentException("Starting salary must be at least 30,000.", nameof(startingSalary));

        if (!role.IsSalaryWithinRange(role, startingSalary))
            throw new ArgumentException("Starting salary is not within the role's salary range.", nameof(startingSalary));

        return new Employee(fullName, role, address, startingSalary);
    }

    public void Promote(Role newRole, Money newSalary)
    {
        if (!newRole.IsSalaryWithinRange(newRole, newSalary))
            throw new ArgumentException("New salary is not within the new role's salary range.", nameof(newSalary));

        Role = newRole;
        Salary = newSalary;
    }

    public void UpdateAddress(Address newAddress)
    {
        Address = newAddress;
    }
}