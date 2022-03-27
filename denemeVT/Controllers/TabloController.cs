using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using denemeVT.Data;
using denemeVT.Models;

namespace denemeVT.Controllers
{
    public class TabloController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TabloController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tablo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tablo.ToListAsync());
        }

        // GET: Tablo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tablo = await _context.Tablo
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tablo == null)
            {
                return NotFound();
            }

            return View(tablo);
        }

        // GET: Tablo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tablo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,metin,sayi")] Tablo tablo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tablo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tablo);
        }

        // GET: Tablo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tablo = await _context.Tablo.FindAsync(id);
            if (tablo == null)
            {
                return NotFound();
            }
            return View(tablo);
        }

        // POST: Tablo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,metin,sayi")] Tablo tablo)
        {
            if (id != tablo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tablo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TabloExists(tablo.ID))
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
            return View(tablo);
        }

        // GET: Tablo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tablo = await _context.Tablo
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tablo == null)
            {
                return NotFound();
            }

            return View(tablo);
        }

        // POST: Tablo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tablo = await _context.Tablo.FindAsync(id);
            _context.Tablo.Remove(tablo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TabloExists(int id)
        {
            return _context.Tablo.Any(e => e.ID == id);
        }
    }
}
