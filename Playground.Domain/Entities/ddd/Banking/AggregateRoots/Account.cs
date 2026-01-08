using Playground.Domain.Entities.ddd.Banking.ValueObjects;

namespace Playground.Domain.Entities.ddd.Banking.AggregateRoots;

public class Account
{
    public Guid Id { get; private set; }
    public AccountNumber AccountNumber { get; private set; }
    public Money Balance { get; private set; }
    public bool IsActive { get; private set; } = true;
    public bool IsFrozen { get; private set; } = false;

    // Navigation Properties
    public Customer Owner { get; private set; }
    public Employee CreatedBy { get; private set; }
    public Employee? FrozenBy { get; private set; }
    public Employee? ClosedBy { get; private set; }

    private Account(AccountNumber accountNumber, Customer owner, Money initialDeposit, Employee createdBy)
    {
        Id = Guid.NewGuid();
        AccountNumber = accountNumber;
        Owner = owner;
        Balance = initialDeposit;
        CreatedBy = createdBy;
    }

    public static Account Open(AccountNumber accountNumber, Customer owner, Money initialDeposit, Employee createdBy)
    {
        if (initialDeposit.Amount < 1000)
            throw new ArgumentException("Initial deposit must be at least 1000.", nameof(initialDeposit));

        return new Account(accountNumber, owner, initialDeposit, createdBy);
    }

    public void Deposit(Money amount)
    {
        if (!IsActive || IsFrozen)
            throw new InvalidOperationException("Cannot deposit to an inactive or frozen account.");

        if (amount.Currency != Balance.Currency)
            throw new ArgumentException("Currency mismatch.", nameof(amount));

        Balance = Balance.Add(amount);
    }

    public void Withdraw(Money amount)
    {
        if (!IsActive || IsFrozen)
            throw new InvalidOperationException("Cannot withdraw from an inactive or frozen account.");

        if (amount.Currency != Balance.Currency)
            throw new ArgumentException("Currency mismatch.", nameof(amount));

        if (amount.Amount > Balance.Amount)
            throw new InvalidOperationException("Insufficient funds.");

        Balance = Balance.Subtract(amount);
    }

    public void Freeze(Employee frozenBy)
    {
        if (!IsActive)
            throw new InvalidOperationException("Cannot freeze an inactive account.");

        IsFrozen = true;
        FrozenBy = frozenBy;
    }

    public void Unfreeze()
    {
        if (!IsActive)
            throw new InvalidOperationException("Cannot unfreeze an inactive account.");

        IsFrozen = false;
        FrozenBy = null;
    }

    public void Close(Employee closedBy)
    {
        if (!IsActive)
            throw new InvalidOperationException("Account is already inactive.");

        IsActive = false;
        ClosedBy = closedBy;
    }
}