using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using dneme2.Models;
using Microsoft.EntityFrameworkCore;

namespace dneme2.Models
{
    [Table("Yük_bilgi")]
    public partial class YükBilgi
    {
        public YükBilgi()
        {
            GemiBilgis = new HashSet<GemiBilgi>();
        }

        [Key]
        [Column("id_yükbil")]
        public int IdYükbil { get; set; }
        [Column("yük_agir")]
        public int? YükAgir { get; set; }
        [Column("yük_miktar")]
        public int? YükMiktar { get; set; }
        [Column("yük_güven")]
        [StringLength(10)]
        public string? YükGüven { get; set; }
        [Column("yuk_tur")]
        [StringLength(20)]
        public string? YukTur { get; set; }

        [InverseProperty("IdYüklerNavigation")]
        public virtual ICollection<GemiBilgi> GemiBilgis { get; set; }
    }
}
