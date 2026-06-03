using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace H_Yonetim
{
    [Table("tbl_hastalar")]
    public class Hasta
    {
        [Key]
        public int HastaID { get; set; }

        [Required]
        [MaxLength(50)]
        public string HastaAd { get; set; }

        [Required]
        [MaxLength(50)]
        public string HastaSoyad { get; set; }

        [Required]
        [MaxLength(50)]
        public string HastaTC { get; set; }

        [Required]
        [MaxLength(50)]
        public string HastaTel { get; set; }

       
        [Column("Doktor_DoktorID")]
        public int? Doktor_DoktorID { get; set; }

      
        [ForeignKey("Doktor_DoktorID")]
        public virtual Doktor Doktor { get; set; }
    }
}