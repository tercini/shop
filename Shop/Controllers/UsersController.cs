using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using Shop.Models.ViewModels;
using Shop.Services;

namespace Shop.Controllers
{
    public class UsersController : Controller
    {
        private readonly ShopContext _context;
        private readonly UserGroupService _userGroupsService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserService _userService;


        public UsersController(ShopContext context, UserGroupService userGroupsService, IHttpContextAccessor httpContextAccessor, UserService userService)
        {
            _context = context;
            _userGroupsService = userGroupsService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public int VerificarAutenticacao()
        {
            string idUser = "0";
            try
            {
                idUser = HttpContext.Session.GetString("IdUsuario");
            }
            catch
            {
                idUser = "0";
            }
            
            if(idUser != "" && idUser  != "0" && idUser != null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
            
        }

        public async Task<IActionResult> Dashboard()
        {
            if (_userService.VerificarAutenticacao() == 0)
            {
                return RedirectToAction("Login", "Users");
            }
            return View(nameof(Dashboard));
        }

        public async Task<IActionResult> Sair()
        {
            HttpContext.Session.SetString("IdUsuario", "0");
            return View("Login");
        }

        // GET: Users
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(/*[Bind("Id,Name,Email,Senha")]*/ User user)
        {
             var vUser = await _context.User.Where(x => x.Email == user.Email && x.Senha == user.Senha).Select(x => new { x.Id, x.UserGroupId, x.Email }).ToListAsync();

            if(vUser.Count != 0)
            {
                HttpContext.Session.SetString("IdUsuario", vUser[0].Id.ToString());
                HttpContext.Session.SetString("UserGroupId", vUser[0].UserGroupId.ToString());

                string teste = HttpContext.Session.GetString("UserGroupId");

                if (vUser[0].UserGroupId == 1)
                    return View("Dashboard");
                else
                    return View("Dashboard");
            }

            HttpContext.Session.SetString("IdUsuario", "0");
            HttpContext.Session.SetString("UserGroupId", "0");

            return View();
        }

        public async Task<IActionResult> Registro()
        {           
            return View();            
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            var userGroups = await _userGroupsService.FindAll();
            var viewModel = new UserViewModel { UserGroups = userGroups };
            return View(viewModel);
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("Id,Name,Email,Senha")]*/ User user)
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Senha")] User user)
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
