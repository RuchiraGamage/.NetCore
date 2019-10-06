using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models.ViewModel
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart shoppingCart { get; set; }
        public decimal shoppingCartTotal { get; set; }
    }
}
