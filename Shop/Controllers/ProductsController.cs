using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using Shop.Models.ViewModels;
using Shop.Services;

namespace Shop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShopContext _context;
        private readonly ProductGroupService _productGroupService;
        private readonly UserService _userService;

        public ProductsController(ShopContext context, ProductGroupService productGroupService, UserService userService)
        {
            _context = context;
            _productGroupService = productGroupService;
            _userService = userService;

        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
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

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            var productGroups = await _productGroupService.FindAll();
            var viewModel = new ProductViewModel { ProductGroups = productGroups };
            return View(viewModel);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("Id,Descricao,Valor,Observacao")]*/ Product product)
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
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

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,Valor,Observacao")] Product product)
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
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

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
