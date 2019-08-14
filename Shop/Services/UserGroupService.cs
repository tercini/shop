using Microsoft.EntityFrameworkCore;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Services
{
    public class UserGroupService
    {
        private readonly ShopContext _context;

        public UserGroupService(ShopContext context)
        {
            _context = context;
        }

        public async Task<List<UserGroup>> FindAll()
        {
            return await (_context.UserGroup.OrderBy(x => x.Descricao).ToListAsync());
        }
    }
}
