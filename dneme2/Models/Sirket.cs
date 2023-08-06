using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using dneme2.Models;
using Microsoft.EntityFrameworkCore;

namespace dneme2.Models
{
    [Table("Sirket")]
    public partial class Sirket
    {
        public Sirket()
        {
            GemiBilgis = new HashSet<GemiBilgi>();
        }

        [Key]
        [Column("id_sir")]
        public int IdSir { get; set; }
        [Column("sirketisim")]
        [StringLength(20)]
        public string? Sirketisim { get; set; }
        [Column("sirket_ülke")]
        [StringLength(10)]
        public string? SirketÜlke { get; set; }
        [Column("Sirket_tec")]
        public int? SirketTec { get; set; }

        [InverseProperty("IdSirketlerNavigation")]
        public virtual ICollection<GemiBilgi> GemiBilgis { get; set; }
    }
}
