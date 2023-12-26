using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using webapp_ZS.Data;
using webapp_ZS.Models;

namespace webapp_ZS.Controllers
{
    public class ArtworksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnviroment;
        public ArtworksController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnviroment = hostEnvironment;
        }

        // GET: Artworks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Artworks.Include(a => a.Artists).Include(a => a.Categories);
          
          
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Artworks/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Artworks == null)
            {
                return NotFound();
            }

            var artworks = await _context.Artworks
                .Include(a => a.Artists)
                .Include(a => a.Categories)
                .FirstOrDefaultAsync(m => m.artworksId == id);
            if (artworks == null)
            {
                return NotFound();
            }

            return View(artworks);
        }
        [Authorize(Roles = "administrator")]

        // GET: Artworks/Create
        public IActionResult Create()
        {
            ViewData["artistId"] = new SelectList(_context.Artists, "Id", "artistName");
            ViewData["categoriesId"] = new SelectList(_context.Categories, "Id", "categoryName");
            return View();
        }

        // POST: Artworks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrator")]
        public async Task<IActionResult> Create([Bind("artworksId,artworksName,artworksCreationDate,artworksMaterials,artworksDimensions,artworksDescripton,artworksPrice,artworksStockQuantity,Dosya,artistId,categoriesId,categoryName")] Artworks artworks)
        {
            ViewData["artistId"] = new SelectList(_context.Artists, "Id", "artistName", artworks.artistId);
            ViewData["categoriesId"] = new SelectList(_context.Categories, "Id", "categoryName", artworks.categoriesId);

            var dosyaYolu = Path.Combine(_hostEnviroment.WebRootPath, "resimler");

            if (!Directory.Exists(dosyaYolu))
            {
                Directory.CreateDirectory(dosyaYolu);
            }

            // Dosya seçilmiş mi kontrolü
            if (artworks.Dosya != null && artworks.Dosya.Length > 0)
            {
                var tamDosyaAdi = Path.Combine(dosyaYolu, artworks.Dosya.FileName);

                // Aynı isimde dosya var mı kontrolü
                if (DosyaAdiKullanimdaMi(artworks.Dosya.FileName))
                {
                    // Aynı isimde dosya var, önceki kaydı sil
                    var eskiKayit = _context.Artworks.FirstOrDefault(a => a.DosyaAdi == artworks.Dosya.FileName);
                    if (eskiKayit != null)
                    {
                        _context.Artworks.Remove(eskiKayit);
                        await _context.SaveChangesAsync();
                    }
                }

                // Yeni dosyayı kaydet
                using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                {
                    await artworks.Dosya.CopyToAsync(dosyaAkisi);
                }
                artworks.DosyaAdi = artworks.Dosya.FileName;
            }
            else
            {
                // Dosya seçilmemiş, DosyaAdi'ni boş bırakın
                artworks.DosyaAdi = string.Empty;
            }

            // Eğer DosyaAdi boş değilse, kaydı ekleyin
            if (!string.IsNullOrEmpty(artworks.DosyaAdi))
            {
                _context.Add(artworks);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DosyaAdiKullanimdaMi(string dosyaAdi)
        {
            return _context.Artworks.Any(a => a.DosyaAdi == dosyaAdi);
        }



        // GET: Artworks/Edit/5
        [Authorize(Roles = "administrator")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Artworks == null)
            {
                return NotFound();
            }

            var artworks = await _context.Artworks.FindAsync(id);
            if (artworks == null)
            {
                return NotFound();
            }


            ViewData["artistId"] = new SelectList(_context.Artists, "Id", "artistName");
            ViewData["categoriesId"] = new SelectList(_context.Categories, "Id", "categoryName");
            return View(artworks);
        }

        // POST: Artworks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("artworksId,artworksName,artworksCreationDate,artworksMaterials,artworksDimensions,artworksDescripton,artworksPrice,artworksStockQuantity,artistId,categoriesId,Dosya,DosyaAdi")] Artworks artworks)
        {
            if (id != artworks.artworksId)
            {
                return NotFound();
            }

            // Dosya eklenmişse
            if (artworks.Dosya != null)
            {
                var dosyaYolu = Path.Combine(_hostEnviroment.WebRootPath, "resimler");

                if (!Directory.Exists(dosyaYolu))
                {
                    Directory.CreateDirectory(dosyaYolu);
                }

                var tamDosyaAdi = Path.Combine(dosyaYolu, artworks.Dosya.FileName);
                using (var dosyaAkisi = new FileStream(tamDosyaAdi, FileMode.Create))
                {
                    await artworks.Dosya.CopyToAsync(dosyaAkisi);
                }

                artworks.DosyaAdi = artworks.Dosya.FileName;
            }
            else
            {
                // Dosya eklenmemişse, mevcut kaydın dosya adını koru
                var existingArtwork = _context.Artworks.AsNoTracking().FirstOrDefault(a => a.artworksId == artworks.artworksId);
                artworks.DosyaAdi = existingArtwork.DosyaAdi;
            }

            // Dosya dışındaki değişiklikleri kontrol etmek için EntityState.Modified kullan
            _context.Entry(artworks).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtworksExists(artworks.artworksId))
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


        // GET: Artworks/Delete/5
        [Authorize(Roles = "administrator")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Artworks == null)
            {
                return NotFound();
            }

            var artworks = await _context.Artworks
                .Include(a => a.Artists)
                .Include(a => a.Categories)
                .FirstOrDefaultAsync(m => m.artworksId == id);
            if (artworks == null)
            {
                return NotFound();
            }

            return View(artworks);
        }

        // POST: Artworks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrator")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Artworks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Artworks'  is null.");
            }
            var artworks = await _context.Artworks.FindAsync(id);
            if (artworks != null)
            {
                _context.Artworks.Remove(artworks);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtworksExists(int id)
        {
            return (_context.Artworks?.Any(e => e.artworksId == id)).GetValueOrDefault();
        }

        [HttpPost("AddCart")]
        public IActionResult AddCart(int artworksId)
        {
            var artwork = _context.Artworks.FirstOrDefault(a => a.artworksId == artworksId);
            if (artwork == null)
            {
                return NotFound("Artwork not found");
            }

            string? userId = User?.Identity?.Name;

            // Define a variable to store the cart ID
            int cartId;

            // Check if the cart for this user and artwork already exists
            var existingCart = _context.Carts.FirstOrDefault(c => c.userIds == userId && c.artworksId == artworksId);

            if (existingCart != null)
            {
                // Cart exists, increase the cartPiece
                existingCart.cartPiece += 1;
                existingCart.cartTotalAmount += artwork.artworksPrice;
                cartId = existingCart.Id; // Set the cartId from the existing cart
            }
            else
            {
                // Cart does not exist, create a new one
                var cart = new Cart
                {
                    artworksId = artworksId,
                    cartTime = DateTime.Now,
                    cartPiece = 1,
                    cartTotalAmount = artwork.artworksPrice,
                    userIds = userId
                };

                _context.Carts.Add(cart);
                _context.SaveChanges(); // Save changes to get the cart ID
                cartId = cart.Id; // Set the cartId from the new cart
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while saving the cart: " + ex.Message);
            }

            return RedirectToAction("Details", "Carts", new { id = cartId });
        }





    }
}
