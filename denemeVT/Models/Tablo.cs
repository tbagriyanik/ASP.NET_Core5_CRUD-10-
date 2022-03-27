using System.ComponentModel.DataAnnotations;

namespace denemeVT.Models
{
    public class Tablo
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Giriş Zorunludur")]
        [Display(Name ="Metin")]
        public string metin { get; set; }
        [Display(Name = "Sayı")]
        public float sayi { get; set; }
    }
}
