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
    public class ArtworkTagsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtworkTagsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ArtworkTags
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ArtworkTags.Include(a => a.Artworks);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ArtworkTags/Details/5
        [Authorize(Roles = "administrator")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ArtworkTags == null)
            {
                return NotFound();
            }

            var artworkTag = await _context.ArtworkTags
                .Include(a => a.Artworks)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artworkTag == null)
            {
                return NotFound();
            }

            return View(artworkTag);
        }

        // GET: ArtworkTags/Create
        [Authorize(Roles = "administrator")]

        public IActionResult Create()
        {
            ViewData["artworkId"] = new SelectList(_context.Artworks, "artworksId", "artworksName");
            return View();
        }

        // POST: ArtworkTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrator")]

        public async Task<IActionResult> Create([Bind("Id,tagName,artworkId")] ArtworkTag artworkTag)
        {
           
            _context.Add(artworkTag);

            ViewData["artworkId"] = new SelectList(_context.Artworks, "artworksId", "artworksName", artworkTag.artworkId);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: ArtworkTags/Edit/5
        [Authorize(Roles = "administrator")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ArtworkTags == null)
            {
                return NotFound();
            }

            var artworkTag = await _context.ArtworkTags.FindAsync(id);
            if (artworkTag == null)
            {
                return NotFound();
            }
            ViewData["artworkId"] = new SelectList(_context.Artworks, "artworksId", "artworksName", artworkTag.artworkId);
            return View(artworkTag);
        }

        // POST: ArtworkTags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrator")]

        public async Task<IActionResult> Edit(int id, [Bind("Id,tagName,artworkId")] ArtworkTag artworkTag)
        {
            if (id != artworkTag.Id)
            {
                return NotFound();
            }


            ViewData["artworkId"] = new SelectList(_context.Artworks, "artworksId", "artworksName", artworkTag.artworkId);
            try
                {
                    _context.Update(artworkTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtworkTagExists(artworkTag.Id))
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

        // GET: ArtworkTags/Delete/5
        [Authorize(Roles = "administrator")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ArtworkTags == null)
            {
                return NotFound();
            }

            var artworkTag = await _context.ArtworkTags
                .Include(a => a.Artworks)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artworkTag == null)
            {
                return NotFound();
            }

            return View(artworkTag);
        }

        // POST: ArtworkTags/Delete/5
        [Authorize(Roles = "administrator")]

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ArtworkTags == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ArtworkTags'  is null.");
            }
            var artworkTag = await _context.ArtworkTags.FindAsync(id);
            if (artworkTag != null)
            {
                _context.ArtworkTags.Remove(artworkTag);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtworkTagExists(int id)
        {
            return (_context.ArtworkTags?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
