namespace Playground.Domain.Events;

public class OrderCreatedEvent
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public double Total { get; set; }
    public DateTime Time { get; set; }

}