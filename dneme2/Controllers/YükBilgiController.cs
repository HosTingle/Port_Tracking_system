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
    public class YükBilgiController : Controller
    {
        private readonly limanContext _context;

        public YükBilgiController(limanContext context)
        {
            _context = context;
        }

        // GET: YükBilgi
        public async Task<IActionResult> Index()
        {
            return _context.YükBilgis != null ?
                        View(await _context.YükBilgis.ToListAsync()) :
                        Problem("Entity set 'limantakipContext.YükBilgis'  is null.");
        }

        // GET: YükBilgi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.YükBilgis == null)
            {
                return NotFound();
            }

            var yükBilgi = await _context.YükBilgis
                .FirstOrDefaultAsync(m => m.IdYükbil == id);
            if (yükBilgi == null)
            {
                return NotFound();
            }

            return View(yükBilgi);
        }

        // GET: YükBilgi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: YükBilgi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdYükbil,YükAgir,YükMiktar,YükGüven,YukTur")] YükBilgi yükBilgi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yükBilgi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(yükBilgi);
        }

        // GET: YükBilgi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.YükBilgis == null)
            {
                return NotFound();
            }

            var yükBilgi = await _context.YükBilgis.FindAsync(id);
            if (yükBilgi == null)
            {
                return NotFound();
            }
            return View(yükBilgi);
        }

        // POST: YükBilgi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdYükbil,YükAgir,YükMiktar,YükGüven,YukTur")] YükBilgi yükBilgi)
        {
            if (id != yükBilgi.IdYükbil)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yükBilgi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YükBilgiExists(yükBilgi.IdYükbil))
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
            return View(yükBilgi);
        }

        // GET: YükBilgi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.YükBilgis == null)
            {
                return NotFound();
            }

            var yükBilgi = await _context.YükBilgis
                .FirstOrDefaultAsync(m => m.IdYükbil == id);
            if (yükBilgi == null)
            {
                return NotFound();
            }

            return View(yükBilgi);
        }

        // POST: YükBilgi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.YükBilgis == null)
            {
                return Problem("Entity set 'limantakipContext.YükBilgis'  is null.");
            }
            var yükBilgi = await _context.YükBilgis.FindAsync(id);
            if (yükBilgi != null)
            {
                _context.YükBilgis.Remove(yükBilgi);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                ViewBag.messageString = yükBilgi!.YukTur!.ToString() +
                    "Yük bilgisi mevcuttur." +
                    "                       " +
                    "Silinme işlemi yapılamaz!!";
                return View("information");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool YükBilgiExists(int id)
        {
            return (_context.YükBilgis?.Any(e => e.IdYükbil == id)).GetValueOrDefault();
        }
    }
}
