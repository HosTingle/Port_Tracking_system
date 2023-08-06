using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using dneme2.Models;

namespace dneme2.Controllers
{
    [Authorize]
    public class GemiBilgisController : Controller
    {
        private readonly limanContext _context;
        private readonly IWebHostEnvironment _hostEnvironmen;
        GemiBilgi cd = new GemiBilgi();
        

        public GemiBilgisController(limanContext context, IWebHostEnvironment hostEnvironmen)
        {
            _context = context;
            this._hostEnvironmen = hostEnvironmen;
        }

        // GET: GemiBilgis
        public async Task<IActionResult> Index()
        {
            var limantakipContext = _context.GemiBilgis.Include(g => g.GemiPerNavigation).Include(g => g.IdSirketlerNavigation).Include(g => g.IdYüklerNavigation);
            return View(await limantakipContext.ToListAsync());
        }

        // GET: GemiBilgis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GemiBilgis == null)
            {
                return NotFound();
            }

            var gemiBilgi = await _context.GemiBilgis
                .Include(g => g.GemiPerNavigation)
                .Include(g => g.IdSirketlerNavigation)
                .Include(g => g.IdYüklerNavigation)
                .FirstOrDefaultAsync(m => m.IdGembilgi == id);
            if (gemiBilgi == null)
            {
                return NotFound();
            }

            return View(gemiBilgi);
        }

        // GET: GemiBilgis/Create
        public IActionResult Create()
        {
            
        
            ViewData["GemiPer"] = new SelectList(_context.Personels, "IdPerson", "Ad");
            ViewData["IdSirketler"] = new SelectList(_context.Sirkets, "IdSir", "Sirketisim");
            ViewData["IdYükler"] = new SelectList(_context.YükBilgis, "IdYükbil", "YukTur");
            return View();
        }
        public IActionResult Mreate()
        {

            List<Bolge> bolgelist = new List<Bolge>();
            cd.RegionList = new SelectList(_context.Bolges, "RegionId", "TerritoryDes");
            cd.TeroList = new SelectList(_context.Teros, "RegionId", "Region");
            return View(cd);
        }

        public JsonResult GetTerritories(int p)
        {
            var territorylist = (from x in _context.Bolges
                                 join y in _context.Teros on x.RegionId equals y.RegionId
                                 where x.RegionId == p
                                 select new
                                 {
                                     Text = x.TerritoryDes,
                                     Value = x.TeritoryId.ToString()
                                 }).ToList();
            return Json(territorylist, new System.Text.Json.JsonSerializerOptions());
        }

        // POST: GemiBilgis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGembilgi,Bulkon,Gitkon,CıkZam,Giris,IdSirketler,IdYükler,GemiAd,ImageFil,GemiPer")] GemiBilgi gemiBilgi)
        {
            //if (ModelState.IsValid)
            // {
            string wwwwrootpath = _hostEnvironmen.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(gemiBilgi.ImageFil.FileName);
            string extension = Path.GetExtension(gemiBilgi.ImageFil.FileName);
            gemiBilgi.Gemifoto = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwwrootpath + "/Contents/", fileName);
            using (var fillestream = new FileStream(path, FileMode.Create))
            {
                await gemiBilgi.ImageFil.CopyToAsync(fillestream);
            }
            _context.Add(gemiBilgi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //  }

            ViewData["GemiPer"] = new SelectList(_context.Personels, "IdPerson", "IdPerson", gemiBilgi.GemiPer);
            ViewData["IdSirketler"] = new SelectList(_context.Sirkets, "IdSir", "IdSir", gemiBilgi.IdSirketler);
            ViewData["IdYükler"] = new SelectList(_context.YükBilgis, "IdYükbil", "IdYükbil", gemiBilgi.IdYükler);
            return View(gemiBilgi);
        }

        // GET: GemiBilgis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GemiBilgis == null)
            {
                return NotFound();
            }

            var gemiBilgi = await _context.GemiBilgis.FindAsync(id);
            if (gemiBilgi == null)
            {
                return NotFound();
            }
            ViewData["GemiPer"] = new SelectList(_context.Personels, "IdPerson", "Ad", gemiBilgi.GemiPer);
            ViewData["IdSirketler"] = new SelectList(_context.Sirkets, "IdSir", "Sirketisim", gemiBilgi.IdSirketler);
            ViewData["IdYükler"] = new SelectList(_context.YükBilgis, "IdYükbil", "YukTur", gemiBilgi.IdYükler);
            return View(gemiBilgi);
        }

        // POST: GemiBilgis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdGembilgi,Bulkon,Gitkon,CıkZam,Giris,IdSirketler,IdYükler,GemiAd,ImageFil,GemiPer")] GemiBilgi gemiBilgi)
        {
            if (id != gemiBilgi.IdGembilgi)
            {
                return NotFound();
            }

            // if (ModelState.IsValid)
            //   {
            string wwwwrootpath = _hostEnvironmen.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(gemiBilgi.ImageFil.FileName);
            string extension = Path.GetExtension(gemiBilgi.ImageFil.FileName);
            gemiBilgi.Gemifoto = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwwrootpath + "/Contents/", fileName);
            using (var fillestream = new FileStream(path, FileMode.Create))
            {
                await gemiBilgi.ImageFil.CopyToAsync(fillestream);
            }
            try
            {
                _context.Update(gemiBilgi);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GemiBilgiExists(gemiBilgi.IdGembilgi))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            //}
            ViewData["GemiPer"] = new SelectList(_context.Personels, "IdPerson", "IdPerson", gemiBilgi.GemiPer);
            ViewData["IdSirketler"] = new SelectList(_context.Sirkets, "IdSir", "IdSir", gemiBilgi.IdSirketler);
            ViewData["IdYükler"] = new SelectList(_context.YükBilgis, "IdYükbil", "IdYükbil", gemiBilgi.IdYükler);
            return View(gemiBilgi);
        }

        // GET: GemiBilgis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GemiBilgis == null)
            {
                return NotFound();
            }

            var gemiBilgi = await _context.GemiBilgis
                .Include(g => g.GemiPerNavigation)
                .Include(g => g.IdSirketlerNavigation)
                .Include(g => g.IdYüklerNavigation)
                .FirstOrDefaultAsync(m => m.IdGembilgi == id);
            if (gemiBilgi == null)
            {
                return NotFound();
            }

            return View(gemiBilgi);
        }

        // POST: GemiBilgis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GemiBilgis == null)
            {
                return Problem("Entity set 'limantakipContext.GemiBilgis'  is null.");
            }
            var gemiBilgi = await _context.GemiBilgis.FindAsync(id);
            if (gemiBilgi != null)
            {
                _context.GemiBilgis.Remove(gemiBilgi);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                ViewBag.messageString = gemiBilgi!.GemiAd!.ToString() +
                    "Gemi bilgisi mevcut.Silinme işlemi yapılamaz!!";
                return View("information");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool GemiBilgiExists(int id)
        {
            return (_context.GemiBilgis?.Any(e => e.IdGembilgi == id)).GetValueOrDefault();
        }
    }
}
