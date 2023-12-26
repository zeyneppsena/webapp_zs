using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapp_ZS.Data;

namespace webapp_ZS.Controllers
{
    [Authorize(Roles = "administrator")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.UserCount = _context.Users.Count(); 
            ViewBag.CartCount = _context.Carts.Count();
            ViewBag.SalesCount = _context.Sales.Count();
            ViewBag.ArtistCount = _context.Artists.Count();
            ViewBag.ProductCount = _context.Artworks.Count(); 
            return View();
        }
    }
}
