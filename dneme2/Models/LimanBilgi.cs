using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dneme2.Models
{
    [Table("Liman_bilgi")]
    public partial class LimanBilgi
    {
        [Key]
        [Column("id_lima")]
        public int IdLima { get; set; }
        [Column("bulseh")]
        [StringLength(20)]
        public string? Bulseh { get; set; }
        [Column("limanad")]
        [StringLength(20)]
        public string? Limanad { get; set; }
        [Column("bulülke")]
        [StringLength(20)]
        public string? Bulülke { get; set; }
        [Column("personsay")]
        public int? Personsay { get; set; }
        [Column("id_gemiler")]
        public int? IdGemiler { get; set; }
        [Column("limanfoto")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Limanfoto { get; set; }
        [NotMapped]
        [DisplayName("Liman Resmi")]
        public IFormFile ImageFi { get; set; }


        [ForeignKey("IdGemiler")]
        [InverseProperty("LimanBilgis")]
        public virtual GemiBilgi? IdGemilerNavigation { get; set; }
    }
}
