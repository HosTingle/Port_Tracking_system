using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dneme2.Models
{
    [Table("Bolge")]
    public partial class Bolge
    {
        [Key]
        [Column("TeritoryID")]
        [StringLength(20)]
        public string TeritoryId { get; set; } = null!;
        [StringLength(10)]
        public string TerritoryDes { get; set; } = null!;
        public int RegionId { get; set; }

        [ForeignKey("RegionId")]
        [InverseProperty("Bolges")]
        public virtual Tero Region { get; set; } = null!;
    }
}
