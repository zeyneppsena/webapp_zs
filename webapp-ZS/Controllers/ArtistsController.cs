using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webapp_ZS.Data;
using webapp_ZS.Models;

namespace webapp_ZS.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Artists
        public async Task<IActionResult> Index()
        {
              return _context.Artists != null ? 
                          View(await _context.Artists.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Artists'  is null.");
        }

        // GET: Artists/Details/5
        [Authorize(Roles = "administrator")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Artists == null)
            {
                return NotFound();
            }

            var artists = await _context.Artists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artists == null)
            {
                return NotFound();
            }

            return View(artists);
        }

        // GET: Artists/Create
        [Authorize(Roles = "administrator")]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrator")]

        public async Task<IActionResult> Create([Bind("Id,artistName,artistBiography,artistContact")] Artists artists)
        {
            
                _context.Add(artists);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: Artists/Edit/5
        [Authorize(Roles = "administrator")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Artists == null)
            {
                return NotFound();
            }

            var artists = await _context.Artists.FindAsync(id);
            if (artists == null)
            {
                return NotFound();
            }
            return View(artists);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrator")]

        public async Task<IActionResult> Edit(int id, [Bind("Id,artistName,artistBiography,artistContact")] Artists artists)
        {
            if (id != artists.Id)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(artists);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistsExists(artists.Id))
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

        [Authorize(Roles = "administrator")]

        // GET: Artists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Artists == null)
            {
                return NotFound();
            }

            var artists = await _context.Artists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artists == null)
            {
                return NotFound();
            }

            return View(artists);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrator")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Artists == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Artists'  is null.");
            }
            var artists = await _context.Artists.FindAsync(id);
            if (artists != null)
            {
                _context.Artists.Remove(artists);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistsExists(int id)
        {
          return (_context.Artists?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
