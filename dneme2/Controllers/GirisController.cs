using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dneme2.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace dneme2.Controllers
{
    public class GirisController : Controller
    {
        private readonly limanContext _context;

        public GirisController(limanContext context)
        {
            _context = context;
        }

        // GET: Giris
        public async Task<IActionResult> Index()
        {
              return _context.Girises != null ? 
                          View(await _context.Girises.ToListAsync()) :
                          Problem("Entity set 'limanContext.Girises'  is null.");
        }

        // GET: Giris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Girises == null)
            {
                return NotFound();
            }

            var giris = await _context.Girises
                .FirstOrDefaultAsync(m => m.IdGir == id);
            if (giris == null)
            {
                return NotFound();
            }

            return View(giris);
        }

        // GET: Giris/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Giris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdGir,Kulad,Sifre,Mail,Ad,Soyad")] Giris giris)
        {
            if (ModelState.IsValid)
            {
                _context.Add(giris);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            return View(giris);
        }

        // GET: Giris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Girises == null)
            {
                return NotFound();
            }

            var giris = await _context.Girises.FindAsync(id);
            if (giris == null)
            {
                return NotFound();
            }
            return View(giris);
        }

        // POST: Giris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdGir,Kulad,Sifre,Mail,Ad,Soyad")] Giris giris)
        {
            if (id != giris.IdGir)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(giris);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GirisExists(giris.IdGir))
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
            return View(giris);
        }

        // GET: Giris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Girises == null)
            {
                return NotFound();
            }

            var giris = await _context.Girises
                .FirstOrDefaultAsync(m => m.IdGir == id);
            if (giris == null)
            {
                return NotFound();
            }

            return View(giris);
        }

        // POST: Giris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Girises == null)
            {
                return Problem("Entity set 'limanContext.Girises'  is null.");
            }
            var giris = await _context.Girises.FindAsync(id);
            if (giris != null)
            {
                _context.Girises.Remove(giris);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GirisExists(int id)
        {
          return (_context.Girises?.Any(e => e.IdGir == id)).GetValueOrDefault();
        }
        [HttpGet]
        public IActionResult Login()
        {
            ClaimsPrincipal claimuser = HttpContext.User;
            if (claimuser.Identity!.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            Hesap _hesap = new Hesap();
            return View(_hesap);
        }
        [HttpPost]
        public async Task<IActionResult> Login(Hesap _hesap)
        {
            limanContext _irisleContext = new limanContext();
            var status = _irisleContext.Girises.Where(m => m.Kulad == _hesap.kulad && m.Sifre == _hesap.sifre).FirstOrDefault();
            if (status == null)
            {
                ViewBag.LoginStatus = 0;
            }
            else
            {
                List<Claim> claims = new List<Claim>()
                {

                    new Claim(ClaimTypes.NameIdentifier,_hesap.sifre)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties prop = new AuthenticationProperties()
                {
                    AllowRefresh = true,

                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), prop);
                return RedirectToAction("Index", "home");

            }
            ViewData["OnayMesaji"] = "Kullanıcı bulunamadı";
            return View(_hesap);
        }
    }
}
