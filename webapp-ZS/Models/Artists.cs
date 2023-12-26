using System.ComponentModel.DataAnnotations;

namespace webapp_ZS.Models
{
    public class Artists
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30)]
        [Display(Name ="Sanatçı Adı")]
        public string artistName { get; set; }

        [Display(Name ="Biyografi")]
        public string artistBiography { get; set; }

        [Display(Name ="İletişim")]
        
        public string artistContact { get; set; }

        virtual public ICollection<Artworks> Artworks { get; set; }
        virtual public ICollection<Event>  Events { get; set; }


    }
}
