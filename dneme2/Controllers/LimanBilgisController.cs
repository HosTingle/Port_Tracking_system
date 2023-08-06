using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using dneme2.Models;

namespace Finalprdfs.Controllers
{
    [Authorize]
    public class LimanBilgisController : Controller
    {
        private readonly limanContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public LimanBilgisController(limanContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: LimanBilgis
        public async Task<IActionResult> Index()
        {
            var limantakipContext = _context.LimanBilgis.Include(l => l.IdGemilerNavigation);
            return View(await limantakipContext.ToListAsync());
        }

        // GET: LimanBilgis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LimanBilgis == null)
            {
                return NotFound();
            }

            var limanBilgi = await _context.LimanBilgis
                .Include(l => l.IdGemilerNavigation)
                .FirstOrDefaultAsync(m => m.IdLima == id);
            if (limanBilgi == null)
            {
                return NotFound();
            }

            return View(limanBilgi);
        }

        // GET: LimanBilgis/Create
        public IActionResult Create()
        {
            ViewData["IdGemiler"] = new SelectList(_context.GemiBilgis, "IdGembilgi", "GemiAd");
            return View();
        }

        // POST: LimanBilgis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLima,Bulseh,Limanad,Bulülke,Personsay,ImageFi,IdGemiler,Limanfoto")] LimanBilgi limanBilgi)
        {
            //if (ModelState.IsValid)
            // {
            string wwwwrootpath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(limanBilgi.ImageFi.FileName);
            string extension = Path.GetExtension(limanBilgi.ImageFi.FileName);
            limanBilgi.Limanfoto = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwwrootpath + "/Contents/", fileName);
            using (var fillestream = new FileStream(path, FileMode.Create))
            {
                await limanBilgi.ImageFi.CopyToAsync(fillestream);
            }
            _context.Add(limanBilgi);
            await _context.SaveChangesAsync();
            ViewData["IdGemiler"] = new SelectList(_context.GemiBilgis, "IdGembilgi", "IdGembilgi", limanBilgi.IdGemiler);
            return RedirectToAction(nameof(Index));
            //  }          
            return View(limanBilgi);
        }

        // GET: LimanBilgis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LimanBilgis == null)
            {
                return NotFound();
            }

            var limanBilgi = await _context.LimanBilgis.FindAsync(id);
            if (limanBilgi == null)
            {
                return NotFound();
            }
            ViewData["IdGemiler"] = new SelectList(_context.GemiBilgis, "IdGembilgi", "GemiAd", limanBilgi.IdGemiler);
            return View(limanBilgi);
        }

        // POST: LimanBilgis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLima,Bulseh,Limanad,Bulülke,Personsay,IdGemiler,ImageFi")] LimanBilgi limanBilgi)
        {
            if (id != limanBilgi.IdLima)
            {
                return NotFound();
            }


            string wwwwrootpath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(limanBilgi.ImageFi.FileName);
            string extension = Path.GetExtension(limanBilgi.ImageFi.FileName);
            limanBilgi.Limanfoto = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwwrootpath + "/Contents/", fileName);
            using (var fillestream = new FileStream(path, FileMode.Create))
            {
                await limanBilgi.ImageFi.CopyToAsync(fillestream);
            }
            try
            {
                _context.Update(limanBilgi);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LimanBilgiExists(limanBilgi.IdLima))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            ViewData["IdGemiler"] = new SelectList(_context.GemiBilgis, "IdGembilgi", "IdGembilgi", limanBilgi.IdGemiler);
            return View(limanBilgi);
        }

        // GET: LimanBilgis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LimanBilgis == null)
            {
                return NotFound();
            }

            var limanBilgi = await _context.LimanBilgis
                .Include(l => l.IdGemilerNavigation)
                .FirstOrDefaultAsync(m => m.IdLima == id);
            if (limanBilgi == null)
            {
                return NotFound();
            }

            return View(limanBilgi);
        }

        // POST: LimanBilgis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LimanBilgis == null)
            {
                return Problem("Entity set 'limantakipContext.LimanBilgis'  is null.");
            }
            var limanBilgi = await _context.LimanBilgis.FindAsync(id);
            if (limanBilgi != null)
            {
                _context.LimanBilgis.Remove(limanBilgi);
            }
            try { await _context.SaveChangesAsync(); }
            catch
            {
                ViewBag.messageString = limanBilgi!.IdGemilerNavigation!.GemiAd!.ToString() +
                    "Gemi girişi mevcuttur.Silinme işlemi yapılamaz!!";
                return View("information");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool LimanBilgiExists(int id)
        {
            return (_context.LimanBilgis?.Any(e => e.IdLima == id)).GetValueOrDefault();
        }
    }
}
