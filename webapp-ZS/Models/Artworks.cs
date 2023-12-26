using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Runtime.InteropServices;

namespace webapp_ZS.Models
{
    public class Artworks
    {
        [Key]
        public int artworksId { get; set; }

        [Display(Name ="Eser Adı")]
        [Required(ErrorMessage ="Boş bırakmayınız")]
        public string artworksName { get; set; }

        [Display(Name ="Oluşturulma Tarihi")]
        public DateTime artworksCreationDate { get; set; } // html sayfasında düzeltilme olacak

        [Display(Name ="Malzemeler")]
        public string artworksMaterials { get; set; }
        [Display(Name ="Boyutlar")]
        public string artworksDimensions { get; set;}
        [Display(Name ="Açıklama")]
        public string artworksDescripton { get; set;}

        [Display(Name ="Fiyatı")]
        public double artworksPrice { get; set; }

        [Display(Name ="Stok Miktari")]
        public int artworksStockQuantity { get; set; }

        [NotMapped]
        public IFormFile Dosya { get; set; }

        public string DosyaAdi { get; set; }
        [ForeignKey("Artists")]
        public int artistId { get; set; } //artist classından bağlantı
        
        virtual public Artists Artists { get; set; } //işaretleme
        
        [ForeignKey("Categories")]

        public int categoriesId { get; set; } //category classından bağlantı
        virtual public Categories Categories { get; set; }  



        public virtual ICollection<ArtworkTag> ArtworkTags { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }


        


    }
}
