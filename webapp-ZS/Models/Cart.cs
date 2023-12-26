using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace webapp_ZS.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tarih")]

        public DateTime cartTime { get; set; }

        [Display(Name = "Adet")]
        public int cartPiece { get; set; }

        [Display(Name = "Toplam Tutar")]

        public double cartTotalAmount { get; set; }

        [Display(Name = "Sanat Eseri")]
        [ForeignKey("Artworks")]
        public int artworksId { get; set; }
        virtual public Artworks artworks { get; set; }

        public string? userIds { get; set; }



        public virtual ICollection<Sale> Sales { get; set; }

    }
}
