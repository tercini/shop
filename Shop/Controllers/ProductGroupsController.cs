﻿using System;
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
    public class ProductGroupsController : Controller
    {
        private readonly ShopContext _context;
        private readonly UserService _userService;

        public ProductGroupsController(ShopContext context, UserService userService)
        {
            _context = context;
            _userService = userService;            
        }

        // GET: ProductGroups
        public async Task<IActionResult> Index()
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }
            return View(await _context.ProductGroup.ToListAsync());
        }

        // GET: ProductGroups/Details/5
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

            var productGroup = await _context.ProductGroup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productGroup == null)
            {
                return NotFound();
            }

            return View(productGroup);
        }

        // GET: ProductGroups/Create
        public IActionResult Create()
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }
            return View();
        }

        // POST: ProductGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao")] ProductGroup productGroup)
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            if (ModelState.IsValid)
            {
                _context.Add(productGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productGroup);
        }

        // GET: ProductGroups/Edit/5
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

            var productGroup = await _context.ProductGroup.FindAsync(id);
            if (productGroup == null)
            {
                return NotFound();
            }
            return View(productGroup);
        }

        // POST: ProductGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao")] ProductGroup productGroup)
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            if (id != productGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductGroupExists(productGroup.Id))
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
            return View(productGroup);
        }

        // GET: ProductGroups/Delete/5
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

            var productGroup = await _context.ProductGroup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productGroup == null)
            {
                return NotFound();
            }

            return View(productGroup);
        }

        // POST: ProductGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            var productGroup = await _context.ProductGroup.FindAsync(id);
            _context.ProductGroup.Remove(productGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductGroupExists(int id)
        {

            return _context.ProductGroup.Any(e => e.Id == id);
        }
    }
}
