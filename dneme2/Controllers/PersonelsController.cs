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
    public class PersonelsController : Controller
    {
        private readonly limanContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public PersonelsController(limanContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Personels
        public async Task<IActionResult> Index()
        {
            return _context.Personels != null ?
                        View(await _context.Personels.ToListAsync()) :
                        Problem("Entity set 'limantakipContext.Personels'  is null.");
        }

        // GET: Personels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Personels == null)
            {
                return NotFound();
            }

            var personel = await _context.Personels
                .FirstOrDefaultAsync(m => m.IdPerson == id);
            if (personel == null)
            {
                return NotFound();
            }

            return View(personel);
        }

        // GET: Personels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPerson,Ad,Soyad,Cinsiyet,ImageFile")] Personel personel)
        {
            // if (ModelState.IsValid)
            // {
            string wwwwrootpath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(personel.ImageFile.FileName);
            string extension = Path.GetExtension(personel.ImageFile.FileName);
            personel.Persfoto = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwwrootpath + "/Contents/", fileName);
            using (var fillestream = new FileStream(path, FileMode.Create))
            {
                await personel.ImageFile.CopyToAsync(fillestream);
            }
            _context.Add(personel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            // }
            return View(personel);
        }

        // GET: Personels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Personels == null)
            {
                return NotFound();
            }

            var personel = await _context.Personels.FindAsync(id);
            if (personel == null)
            {
                return NotFound();
            }
            return View(personel);
        }

        // POST: Personels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPerson,Ad,Soyad,Cinsiyet,ImageFile")] Personel personel)
        {
            if (id != personel.IdPerson)
            {
                return NotFound();
            }

            //   if (ModelState.IsValid)
            //    {
            string wwwwrootpath = _hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(personel.ImageFile.FileName);
            string extension = Path.GetExtension(personel.ImageFile.FileName);
            personel.Persfoto = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwwrootpath + "/Contents/", fileName);
            using (var fillestream = new FileStream(path, FileMode.Create))
            {
                await personel.ImageFile.CopyToAsync(fillestream);
            }
            try
            {
                _context.Update(personel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonelExists(personel.IdPerson))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }


                //    }
            }
            return RedirectToAction(nameof(Index));
            return View(personel);
        }

        // GET: Personels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Personels == null)
            {
                return NotFound();
            }

            var personel = await _context.Personels
                .FirstOrDefaultAsync(m => m.IdPerson == id);
            if (personel == null)
            {
                return NotFound();
            }

            return View(personel);
        }

        // POST: Personels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Personels == null)
            {
                return Problem("Entity set 'limantakipContext.Personels'  is null.");
            }
            var personel = await _context.Personels.FindAsync(id);
            if (personel != null)
            {
                _context.Personels.Remove(personel);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                ViewBag.messageString = personel!.Ad!.ToString() +
                    "İsim bilgisi mevcuttur." +
                    "Silinme işlemi yapılamaz!!";
                return View("information");
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonelExists(int id)
        {
            return (_context.Personels?.Any(e => e.IdPerson == id)).GetValueOrDefault();
        }
    }
}
