using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using dneme2.Models;

namespace dneme2.Controllers
{

    [Authorize]
    public class SirketsController : Controller
    {
        private readonly limanContext _context;

        public SirketsController(limanContext context)
        {
            _context = context;
        }

        // GET: Sirkets
        public async Task<IActionResult> Index()
        {
            return _context.Sirkets != null ?
                        View(await _context.Sirkets.ToListAsync()) :
                        Problem("Entity set 'limantakipContext.Sirkets'  is null.");
        }

        // GET: Sirkets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sirkets == null)
            {
                return NotFound();
            }

            var sirket = await _context.Sirkets
                .FirstOrDefaultAsync(m => m.IdSir == id);
            if (sirket == null)
            {
                return NotFound();
            }

            return View(sirket);
        }

        // GET: Sirkets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sirkets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSir,Sirketisim,SirketÜlke,SirketTec")] Sirket sirket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sirket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sirket);
        }

        // GET: Sirkets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sirkets == null)
            {
                return NotFound();
            }

            var sirket = await _context.Sirkets.FindAsync(id);
            if (sirket == null)
            {
                return NotFound();
            }
            return View(sirket);
        }

        // POST: Sirkets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSir,Sirketisim,SirketÜlke,SirketTec")] Sirket sirket)
        {
            if (id != sirket.IdSir)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sirket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SirketExists(sirket.IdSir))
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
            return View(sirket);
        }

        // GET: Sirkets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sirkets == null)
            {
                return NotFound();
            }

            var sirket = await _context.Sirkets
                .FirstOrDefaultAsync(m => m.IdSir == id);
            if (sirket == null)
            {
                return NotFound();
            }

            return View(sirket);
        }

        // POST: Sirkets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sirkets == null)
            {
                return Problem("Entity set 'limantakipContext.Sirkets'  is null.");
            }
            var sirket = await _context.Sirkets.FindAsync(id);
            if (sirket != null)
            {
                _context.Sirkets.Remove(sirket);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                ViewBag.mas = sirket.Sirketisim.ToString() +
                    "Sirket ismi mevcut." +
                    "Silinme işlemi yapılamaz!!";
                return View("information");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SirketExists(int id)
        {
            return (_context.Sirkets?.Any(e => e.IdSir == id)).GetValueOrDefault();
        }
    }
}
