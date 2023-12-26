using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp_ZS.Models
{
    public class ArtworkTag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string tagName { get; set; }

        [ForeignKey("Artworks")]
        public int artworkId { get; set; } 
        virtual public Artworks Artworks { get; set; }
    }
}
