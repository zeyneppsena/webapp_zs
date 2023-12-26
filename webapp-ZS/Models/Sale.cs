using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp_ZS.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Müşter Adı")]
        public string customerName { get; set; }

        [Display(Name = "Şehir")]
        public string saleCity { get; set; }

        [Display(Name = "Adres")]
        public string saleAdress { get; set; }

        [Display(Name = "Sanat Eseri")]
        [ForeignKey("artworks")]
        public int artworksId { get; set; }
        public virtual Artworks artworks { get; set; }

        [Display(Name = "Adet")]
        public int cartPiece { get; set; }
        [Display(Name = "Toplam Tutar")]
        public decimal cartTotalAmount { get; set; } 
        public string? userIds { get; set; }
    }
}
