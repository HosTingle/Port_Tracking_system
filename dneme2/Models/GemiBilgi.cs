using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace dneme2.Models
{
    [Table("Gemi_bilgi")]
    public partial class GemiBilgi
    {
        public GemiBilgi()
        {
            LimanBilgis = new HashSet<LimanBilgi>();
        }

        [Key]
        [Column("id_gembilgi")]
        public int IdGembilgi { get; set; }
        [Column("bulkon")]
        [StringLength(20)]
        public string? Bulkon { get; set; }
        [Column("gitkon")]
        [StringLength(20)]
        public string? Gitkon { get; set; }
        [Column("cık_zam", TypeName = "date")]
        public DateTime? CıkZam { get; set; }
        [Column("giris", TypeName = "date")]
        public DateTime? Giris { get; set; }
        [Column("id_sirketler")]
        public int? IdSirketler { get; set; }
        [Column("id_yükler")]
        public int? IdYükler { get; set; }
        [Column("gemi_ad")]
        [StringLength(20)]
        public string? GemiAd { get; set; }
        [Column("gemifoto")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Gemifoto { get; set; }
        [NotMapped]
        [DisplayName("Gemi'nin Resmi")]
        public IFormFile ImageFil { get; set; }
        [Column("gemi_per")]
        public int? GemiPer { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> RegionList { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> TeroList { get; set; }
        [ForeignKey("GemiPer")]
        [InverseProperty("GemiBilgis")]
        public virtual Personel? GemiPerNavigation { get; set; }
        [ForeignKey("IdSirketler")]
        [InverseProperty("GemiBilgis")]
        public virtual Sirket? IdSirketlerNavigation { get; set; }
        [ForeignKey("IdYükler")]
        [InverseProperty("GemiBilgis")]
        public virtual YükBilgi? IdYüklerNavigation { get; set; }
        [InverseProperty("IdGemilerNavigation")]
        public virtual ICollection<LimanBilgi> LimanBilgis { get; set; }
    }
}
