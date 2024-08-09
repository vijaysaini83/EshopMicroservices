using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domin
{
    public class OrderCreatedEventHandler
        (IPublisher publisherEndpoint, IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> logger)
        : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domin Event handled : {DominEvent}", domainEvent.GetType().Name);
            if (await featureManager.IsEnabledAsync("OrderFullfilment"))
            {
                var orderCreatedIntegrationEvent = domainEvent.order.ToOrderDto();
                await publisherEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
            }

        }
    }
}
