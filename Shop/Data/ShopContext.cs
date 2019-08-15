using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Models
{
    public class ShopContext : DbContext
    {
        public ShopContext (DbContextOptions<ShopContext> options)
            : base(options)
        {
        }

        public DbSet<Shop.Models.UserGroup> UserGroup { get; set; }
        public DbSet<Shop.Models.User> User { get; set; }
        public DbSet<Shop.Models.ProductGroup> ProductGroup { get; set; }
        public DbSet<Shop.Models.Product> Product { get; set; }
        public DbSet<Shop.Models.Address> Address { get; set; }
        public DbSet<Shop.Models.Cart> Cart { get; set; }
    }
}
