using CakeShop.Models.OrderModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models
{

    /*
         public class AppDbcontext :DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) :base(options)
        {
        }
     }
   */

    //here we use Identity for autherization and authentication
    public class AppDbcontext :IdentityDbContext<IdentityUser>
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) :base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Pie> Pies { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
