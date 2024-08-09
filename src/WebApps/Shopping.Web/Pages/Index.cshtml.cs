
namespace Shopping.Web.Pages
{
    public class IndexModel : PageModel
    {
        private ILogger _logger;
        private ICatalogService _catalogService;
        private IBasketService _basketService;
        public IndexModel(ICatalogService catalogService, IBasketService basketService, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _catalogService = catalogService;
            _basketService = basketService;
        }
        public IEnumerable<ProductModel> ProductList { get; set; } = new List<ProductModel>();
        public async Task<IActionResult> OnGetAsync()
        {
            _logger.LogInformation("Index page visited");
            var result = await _catalogService.GetProducts();
            ProductList = result.Products;
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
        {
            _logger.LogInformation("Add to cart button clicked");
            var productResponse = await _catalogService.GetProduct(productId);
            var basket = await _basketService.LoadUserBasket();

            basket.Items.Add(new ShoppingCartItemModel
            {
                ProductId = productId,
                ProductName = productResponse.Product.Name,
                Price = productResponse.Product.Price,
                Quantity = 1,
                Color = "Black"
            });

            await _basketService.StoreBasket(new StoreBasketRequest(basket));
            return RedirectToPage("Cart");
        }
    }
}
