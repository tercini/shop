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
    public class AddressesController : Controller
    {
        private readonly ShopContext _context;
        private readonly UserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddressesController(ShopContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }


        public async Task<IActionResult> MyAddress()
        {
            if (_userService.VerificarAutenticacao() == 0)
            {
                return RedirectToAction("Login", "Users");
            }
            int idUser = 0;
            try
            {
                 idUser = Convert.ToInt32(HttpContext.Session.GetString("IdUsuario"));
            }
            catch
            {

            }


            return View(await _context.Address.Where(x => x.UserId == idUser).ToListAsync());
            
        }

        public async Task<IActionResult> AddressUser()
        {
            if (_userService.VerificarAutenticacao() == 0)
            {
                return RedirectToAction("Login", "Users");
            }

            return View();
        }


        // GET: Addresses
        public async Task<IActionResult> Index()
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            return View(await _context.Address.ToListAsync());
        }

        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (_userService.VerificarAutenticacao() == 0)
            {
                return RedirectToAction("Login", "Users");
            }

            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address
                .FirstOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: Addresses/Create
        public async Task<IActionResult> Create()
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            var users = await _userService.FindAll();
            var viewModel = new AddressViewModel { User = users };
            return View(viewModel);
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("Id,State,City,Neighborhood,Street,Number,Telephone")]*/ Address address)
        {
            if (_userService.VerificarAutenticacao() == 0)
            {
                return RedirectToAction("Login", "Users");
            }

            try { address.UserId = Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")); } catch { }

            try
            {
                if (HttpContext.Session.GetString("UserGroupId") == "2")
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(address);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                if (HttpContext.Session.GetString("UserGroupId") == "1")
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(address);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("MyAddress", "Addresses");
                    }
                }
            }
            catch
            {

            }
            
            
            
            return View(address);
        }

        // GET: Addresses/Edit/5
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

            var address = await _context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,State,City,Neighborhood,Street,Number,Telephone")] Address address)
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            if (id != address.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.Id))
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
            return View(address);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (_userService.VerificarAutenticacao() == 0 )
            {
                return RedirectToAction("Login", "Users");
            }

            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address
                .FirstOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_userService.VerificarAutenticacao() == 0 )
            {
                return RedirectToAction("Login", "Users");
            }

            var address = await _context.Address.FindAsync(id);
            _context.Address.Remove(address);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(int id)
        {
            return _context.Address.Any(e => e.Id == id);
        }
    }
}
