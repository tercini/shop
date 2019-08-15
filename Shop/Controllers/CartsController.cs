using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using Shop.Services;

namespace Shop.Controllers
{
    public class CartsController : Controller
    {
        private readonly ShopContext _context;
        private readonly UserService _userService;

        public CartsController(ShopContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            if (_userService.VerificarAutenticacao() == 0)
            {
                return RedirectToAction("Login", "Users");
            }

            var shopContext = _context.Cart.Include(c => c.User);
            return View(await shopContext.ToListAsync());
        }

        // GET: Carts/Details/5
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

            var cart = await _context.Cart
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            if (_userService.VerificarAutenticacao() == 0)
            {
                return RedirectToAction("Login", "Users");
            }

            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,Total,UserId")] Cart cart)
        {
            if (_userService.VerificarAutenticacao() == 0)
            {
                return RedirectToAction("Login", "Users");
            }

            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email", cart.UserId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (_userService.VerificarAutenticacao() == 0)
            {
                return RedirectToAction("Login", "Users");
            }

            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email", cart.UserId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,Total,UserId")] Cart cart)
        {
            if (_userService.VerificarAutenticacao() == 0)
            {
                return RedirectToAction("Login", "Users");
            }

            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
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
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Email", cart.UserId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (_userService.VerificarAutenticacao() == 0)
            {
                return RedirectToAction("Login", "Users");
            }

            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_userService.VerificarAutenticacao() == 0)
            {
                return RedirectToAction("Login", "Users");
            }

            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.Id == id);
        }
    }
}
