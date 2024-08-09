
namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrdersByCustoerHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetOrdersByCustoerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustoerQuery query, CancellationToken cancellationToken)
        {
            //get orders by customer using dbContext
            //return result

            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.CustomerId == CustomerId.Of(query.CustoerId))
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);

            return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
        }
    }
}
