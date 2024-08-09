

namespace Catalog.API.Products.GetProductById
{
    public record GetProductbyIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductbyIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductbyIdQuery query, CancellationToken cancellationToken)
        {            
            var product  = await session.LoadAsync<Product>(query.Id,cancellationToken);
            if(product is null)
            {
                throw new ProductNotFoundException(query.Id);
            }
            return new GetProductByIdResult(product);
        }
    }
}
