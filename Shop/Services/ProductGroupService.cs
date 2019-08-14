using Microsoft.EntityFrameworkCore;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Services
{
    public class ProductGroupService
    {
        private readonly ShopContext _context;

        public ProductGroupService(ShopContext context)
        {
            _context = context;
        }

        public async Task<List<ProductGroup>> FindAll()
        {
            return await (_context.ProductGroup.OrderBy(x => x.Descricao).ToListAsync());
        }
    }
}
