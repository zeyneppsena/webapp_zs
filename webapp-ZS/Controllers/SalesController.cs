using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webapp_ZS.Data;
using webapp_ZS.Models;

namespace webapp_ZS.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sales/Index2/5
        public async Task<IActionResult> Index2(int? id)
        {
            if (id == null)
            {
                // Handle the case where no ID is provided (e.g., redirect to a different page or show an error)
                return RedirectToAction("Index"); // or return NotFound();
            }

            var sale = await _context.Sales.Include(s => s.artworks)
                                           .FirstOrDefaultAsync(s => s.Id == id);

            if (sale == null)
            {
                // Handle the case where no sale is found with the provided ID
                return NotFound();
            }

            // Create a list containing only the specified sale
            var salesList = new List<Sale> { sale };
            return View(salesList);
        }


        // GET: Sales
        public async Task<IActionResult> Index()
        {
            IQueryable<Sale> salesQuery = _context.Sales.Include(s => s.artworks);

            // Check if the user is an administrator
            if (User.IsInRole("administrator"))
            {
                // If the user is an administrator, show all sales
            }
            else
            {
                // If the user is not an administrator, filter the sales based on the user's ID
                string? userId = User.Identity?.Name;
                salesQuery = salesQuery.Where(s => s.userIds == userId);
            }

            return View(await salesQuery.ToListAsync());
        }


        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }



            var sale = await _context.Sales
                .Include(s => s.artworks)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            ViewData["artworksId"] = new SelectList(_context.Artworks, "artworksId", "artworksName");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,customerName,saleCity,saleAdress,artworksId,cartPiece,cartTotalAmount,userIds")] Sale sale)
        {

            string? userId = User?.Identity?.Name;
            sale.userIds = userId;

            _context.Add(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            ViewData["artworksId"] = new SelectList(_context.Artworks, "artworksId", "artworksName", sale.artworksId);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            ViewData["artworksId"] = new SelectList(_context.Artworks, "artworksId", "artworksName", sale.artworksId);
            return View(sale);
        }

        // POST: Sales/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,customerName,saleCity,saleAdress")] Sale sale)
        {
            if (id != sale.Id)
            {
                return NotFound();
            }

            try
            {
                // Load the existing sale from the database
                var existingSale = await _context.Sales.FirstOrDefaultAsync(s => s.Id == id);
                if (existingSale == null)
                {
                    return NotFound();
                }

                // Update the specific fields
                existingSale.customerName = sale.customerName;
                existingSale.saleCity = sale.saleCity;
                existingSale.saleAdress = sale.saleAdress;

                // Save changes
                _context.Update(existingSale);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(sale.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index2));
        }




        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sales == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.artworks)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sales == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Sales'  is null.");
            }
            var sale = await _context.Sales.FindAsync(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleExists(int id)
        {
          return (_context.Sales?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
