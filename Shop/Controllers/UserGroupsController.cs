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
    public class UserGroupsController : Controller
    {
        private readonly ShopContext _context;
        private readonly UserService _userService;

        public UserGroupsController(ShopContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public List<UserGroup> FindAll()
        {
            return ( _context.UserGroup.OrderBy(x => x.Descricao).ToList());
        }

        // GET: UserGroups
        public async Task<IActionResult> Index()
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            return View(await _context.UserGroup.ToListAsync());
        }

        // GET: UserGroups/Details/5
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

            var userGroup = await _context.UserGroup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userGroup == null)
            {
                return NotFound();
            }

            return View(userGroup);
        }

        // GET: UserGroups/Create
        public IActionResult Create()
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            return View();
        }

        // POST: UserGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao")] UserGroup userGroup)
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            if (ModelState.IsValid)
            {
                _context.Add(userGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userGroup);
        }

        // GET: UserGroups/Edit/5
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

            var userGroup = await _context.UserGroup.FindAsync(id);
            if (userGroup == null)
            {
                return NotFound();
            }
            return View(userGroup);
        }

        // POST: UserGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao")] UserGroup userGroup)
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            if (id != userGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserGroupExists(userGroup.Id))
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
            return View(userGroup);
        }

        // GET: UserGroups/Delete/5
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

            var userGroup = await _context.UserGroup
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userGroup == null)
            {
                return NotFound();
            }

            return View(userGroup);
        }

        // POST: UserGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_userService.VerificarAutenticacao() == 0 || _userService.VerificarAutenticacao() == 1)
            {
                return RedirectToAction("Login", "Users");
            }

            var userGroup = await _context.UserGroup.FindAsync(id);
            _context.UserGroup.Remove(userGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserGroupExists(int id)
        {
            return _context.UserGroup.Any(e => e.Id == id);
        }
    }
}
