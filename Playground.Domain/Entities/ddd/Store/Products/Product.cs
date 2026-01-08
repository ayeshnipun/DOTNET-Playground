using Playground.Domain.Entities.ddd.ValueObjects;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty!;
    public string Description { get; private set; } = string.Empty!;
    public Money Price { get; private set; } = null!;
    public Sku Sku { get; private set; } = null!;
}
