using Playground.Domain.Entities.ddd.Banking.ValueObjects;

namespace Playground.Domain.Entities.ddd.Banking.AggregateRoots;

public class Customer
{
    public Guid Id { get; private set; }
    public Email Email { get; private set; }
    public Name FullName { get; private set; }
    public Address Address { get; private set; }
    public bool IsActive { get; private set; } = true;

    private Customer(Email email, Name fullName, Address address)
    {
        Id = Guid.NewGuid();
        Email = email;
        FullName = fullName;
        Address = address;
    }

    public static Customer Register(Email email, Name fullName, Address address)
    {
        return new Customer(email, fullName, address);
    }

    public void UpdateInfo(Email newEmail, Name newFullName, Address newAddress)
    {
        if (!IsActive)
            throw new InvalidOperationException("Cannot update information of an inactive customer.");

        Email = newEmail;
        FullName = newFullName;
        Address = newAddress;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}