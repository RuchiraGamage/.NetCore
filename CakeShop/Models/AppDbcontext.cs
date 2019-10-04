using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models
{
    public class AppDbcontext :DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) :base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Pie> Pies { get; set; }
    }
}
