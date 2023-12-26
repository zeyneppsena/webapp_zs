using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webapp_ZS.Data;
using webapp_ZS.Models;

namespace webapp_ZS.Controllers
{
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Carts.Include(c => c.artworks);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Carts == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.artworks)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["artworksId"] = new SelectList(_context.Artworks, "artworksId", "artworksName");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,cartPiece,artworksId")] Cart cart)
        {
            ViewData["artworksId"] = new SelectList(_context.Artworks, "artworksId", "artworksName", cart.artworksId);
            cart.cartTime = DateTime.Now;
            Artworks selectedArtwork = await _context.Artworks.FindAsync(cart.artworksId);
            if (selectedArtwork.artworksStockQuantity >= cart.cartPiece)
            {
                // Calculate the total amount based on the artwork's price and sale piece
                cart.cartTotalAmount = selectedArtwork.artworksPrice * cart.cartPiece;
                selectedArtwork.artworksStockQuantity -= cart.cartPiece;
                // total değeri düşür

            }
            else
            {

                return RedirectToAction("Error");

            }
            string? userId = User?.Identity?.Name;
            cart.userIds = userId;

            _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Carts == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            var artwork = await _context.Artworks.FindAsync(cart.artworksId);
            if (artwork == null)
            {
                // Artwork bulunamazsa, bir hata mesajı döndürülebilir.
                return NotFound("Artwork not found");
            }

            // Cart'ın toplam tutarını hesapla
            cart.cartTotalAmount = artwork.artworksPrice * cart.cartPiece;
            string? userId = User?.Identity?.Name;
            cart.userIds = userId;


            ViewData["artworksId"] = new SelectList(_context.Artworks, "artworksId", "artworksName", cart.artworksId);
            return View(cart);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,cartTime,cartPiece,artworksId")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            var artwork = await _context.Artworks.FindAsync(cart.artworksId);
            if (artwork == null)
            {
                // Artwork bulunamazsa, bir hata mesajı döndürülebilir.
                return NotFound("Artwork not found");
            }

            // Cart'ın toplam tutarını hesapla
            cart.cartTotalAmount = artwork.artworksPrice * cart.cartPiece;

            ViewData["artworksId"] = new SelectList(_context.Artworks, "artworksId", "artworksName", cart.artworksId);

            try
            {
                _context.Update(cart);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(cart.Id))
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


        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Carts == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.artworks)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Carts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Carts'  is null.");
            }
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
          return (_context.Carts?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: Carts/AddSales/5
        public async Task<IActionResult> AddSales(int? id)
        {
            var cart = await _context.Carts
             .FirstOrDefaultAsync(c => c.Id == id);

            if (cart == null)
            {
                return NotFound();
            }
            string? userId = User.Identity?.Name;

            var sale = new Sale
            {
                customerName = "", 
                saleCity = "", 
                saleAdress = "", 
                artworksId = cart.artworksId, 
                cartPiece = cart.cartPiece, 
                cartTotalAmount = (decimal)cart.cartTotalAmount, 
            };

            if (!string.IsNullOrEmpty(userId))
            {
                sale.userIds = userId;
                sale.customerName = userId;
            }


            _context.Sales.Add(sale);
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();



            TempData["SuccessMessage"] = "Satış başarıyla oluşturuldu!";
            return RedirectToAction("Edit", "Sales", new { id = sale.Id });
        }


    }
}
