using Playground.Domain.Entities.ddd.ValueObjects;

public class Order
{
    private readonly HashSet<LineItem> _lineItems = new();
    public IReadOnlyCollection<LineItem> LineItems => _lineItems.AsReadOnly();
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public DateTime OrderDate { get; private set; }
    public Money TotalAmount { get; private set; } = null!;

    // Navigation property
    public Customer Customer { get; private set; } = null!;

    public static Order Create(Guid customerId, DateTime orderDate)
    {
        return new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            OrderDate = orderDate
        };
    }

    public void AddLineItem(Product product)
    {
        var lineItem = new LineItem(Guid.NewGuid(), Id, product.Id, product.Price);
        _lineItems.Add(lineItem);
    }
}

public class LineItem
{
    internal LineItem(Guid id, Guid orderId, Guid productId, Money unitPrice)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        UnitPrice = unitPrice;
    }

    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public Money UnitPrice { get; private set; } = null!;

    // Navigation property
    public Order Order { get; private set; } = null!;
}