using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dneme2.Models
{
    [Table("Personel")]
    public partial class Personel
    {
        public Personel()
        {
            GemiBilgis = new HashSet<GemiBilgi>();
        }

        [Key]
        [Column("id_person")]
        public int IdPerson { get; set; }
        [Column("ad")]
        [StringLength(20)]
        public string? Ad { get; set; }
        [Column("soyad")]
        [StringLength(20)]
        public string? Soyad { get; set; }
        [Column("cinsiyet")]
        [StringLength(1)]
        [Unicode(false)]
        public string? Cinsiyet { get; set; }
        [Column("persfoto")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Persfoto { get; set; }

        [InverseProperty("GemiPerNavigation")]
        public virtual ICollection<GemiBilgi> GemiBilgis { get; set; }
        [NotMapped]
        [DisplayName("Kapta'nın Resmi")]
        public IFormFile ImageFile { get; set; }
    }
}
