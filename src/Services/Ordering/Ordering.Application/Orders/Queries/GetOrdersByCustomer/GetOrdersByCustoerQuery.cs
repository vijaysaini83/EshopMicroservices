namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public record GetOrdersByCustoerQuery(Guid CustoerId): IQuery<GetOrdersByCustomerResult>;
    public record GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);    
}
