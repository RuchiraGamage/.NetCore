using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CakeShop.Models.OrderModels;

namespace CakeShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        public readonly AppDbcontext _appDbcontext;
        public readonly ShoppingCart _shoppingCart;

        public OrderRepository(AppDbcontext appDbcontext, ShoppingCart shoppingCart)
        {
            _appDbcontext = appDbcontext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            _appDbcontext.Orders.Add(order);

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.Pie.Id,
                    OrderId = order.OrderId,
                    Price = shoppingCartItem.Pie.Price
                };

                _appDbcontext.OrderDetails.Add(orderDetail);
            }
            _appDbcontext.SaveChanges();
        }
    }
}
