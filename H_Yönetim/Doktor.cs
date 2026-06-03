using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace H_Yonetim
{
    [Table("tbl_Doktorlar")]
    public class Doktor
    {
        [Key]
        public int DoktorID { get; set; }

        [Required]
        [MaxLength(50)]
        public string DoktorAd { get; set; }

        [Required]
        [MaxLength(50)]
        public string DoktorSoyad { get; set; }

        [Required]
        [MaxLength(11)]
        public string DoktorTC { get; set; }

        [Required]
        [MaxLength(11)]
        public string DoktorTel { get; set; }

        [MaxLength(50)]
        public string DoktorMail { get; set; }

        public int? DoktorBolum { get; set; }

        [ForeignKey("DoktorBolum")]
        public virtual Bolum Bolum { get; set; }
    }
}
