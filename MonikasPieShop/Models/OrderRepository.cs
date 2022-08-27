namespace MonikasPieShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MonikasPieShopDbContext _monikasPieShopDbContext;
        private readonly IShoppingCart _shoppingCart;

        public OrderRepository(MonikasPieShopDbContext monikasPieShopDbContext, IShoppingCart shoppingCart)
        {
            _monikasPieShopDbContext = monikasPieShopDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();

            //adding the order with its details

            foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.Pie.PieId,
                    Price = shoppingCartItem.Pie.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

            _monikasPieShopDbContext.Orders.Add(order);

            _monikasPieShopDbContext.SaveChanges();
        }
    }
}

