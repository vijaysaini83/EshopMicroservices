namespace Ordering.Application.Orders.EventHandlers.Domin
{
    public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger) : INotificationHandler<OrderUpdatedEvent>
    {
        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domin Event handled : {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
