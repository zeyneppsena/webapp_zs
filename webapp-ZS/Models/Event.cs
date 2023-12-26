using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp_ZS.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Etkinlik Adı")]
        [Required]
        public string eventName { get; set; }

        [Display(Name = "Etkinlik Tarihi")]
        [Required]
        public DateTime eventTime { get; set; }

        [Display(Name = "Etkinlik Detay")]
        public string eventDescription { get; set; }

        [Display(Name = "Etkiniği verecek Sanatçı")]
        [ForeignKey("Artists")]
        public int artistId { get; set; }
        virtual public Artists Artists { get; set; }

    }
}
