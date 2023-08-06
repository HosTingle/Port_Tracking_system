using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dneme2.Models
{
    [Table("Tero")]
    public partial class Tero
    {
        public Tero()
        {
            Bolges = new HashSet<Bolge>();
        }

        [Key]
        [Column("RegionID")]
        public int RegionId { get; set; }
        [StringLength(10)]
        public string Region { get; set; } = null!;

        [InverseProperty("Region")]
        public virtual ICollection<Bolge> Bolges { get; set; }
    }
}
