using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Permissions;
using webapp_ZS.Models;

namespace webapp_ZS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Artists> Artists { get; set; }
        public DbSet<Categories> Categories { get; set; }

        public DbSet<Artworks> Artworks { get; set; }
        public DbSet<Event> Events { get; set; }

        public DbSet<ArtworkTag> ArtworkTags { get; set; }  

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Cart> Carts { get; set; }
    }
}