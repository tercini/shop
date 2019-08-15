using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Services
{
    public class UserService
    {
        private readonly ShopContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(ShopContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<User>> FindAll()
        {
            return await (_context.User.OrderBy(x => x.Email).ToListAsync());
        }

        public int VerificarAutenticacao()
        {
            string idUser = "0";
            string idUserGroup = "0";
            try
            {
                idUser = _httpContextAccessor.HttpContext.Session.GetString("IdUsuario");
                idUserGroup = _httpContextAccessor.HttpContext.Session.GetString("UserGroupId");
            }
            catch
            {
                idUser = "0";
            }

            if (idUser != "" && idUser != "0" && idUser != null)
            {
                if (idUserGroup == "1")
                    return 1;
                else if (idUserGroup == "2")
                    return 2;                    
            }
            else
            {
                return 0;
            }

            return 0;
        }
    }
}
