using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class DispatchDominEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDominEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchDominEvents(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public async Task DispatchDominEvents(DbContext? context)
        {
            if (context == null) return;

            var aggregates = context.ChangeTracker
                .Entries<IAggregate>()
                .Where(a => a.Entity.DomainEvents.Any())
                .Select(a => a.Entity);

            var dominEvents = aggregates
                .SelectMany(a => a.DomainEvents).ToList();

            aggregates.ToList().ForEach(a => a.ClearDomainEvents());

            foreach (var domainEvent in dominEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
