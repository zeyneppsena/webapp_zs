using System.ComponentModel.DataAnnotations;

namespace webapp_ZS.Models
{
    public class Categories
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Kategori Adı")]
        public string categoryName { get; set; }

        [Display(Name = "Kategori Açıklama")]
        public string categoryDescription { get; set; }

        public virtual ICollection<Artworks> Artworks { get; set; }
    }
}
