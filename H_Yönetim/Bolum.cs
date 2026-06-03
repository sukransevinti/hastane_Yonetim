using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace H_Yonetim
{
    [Table("tbl_Bolumler")]
    public class Bolum
    {
        [Key]
        public int BolumID { get; set; }

        [Required]
        [MaxLength(50)]
        public string BolumAd { get; set; }

        
        public virtual ICollection<Doktor> Doktorlar { get; set; }
        public virtual ICollection<Hasta> Hastalar { get; set; }
    }
}
