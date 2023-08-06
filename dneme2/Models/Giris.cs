using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dneme2.Models
{
    [Table("Giris")]
    public partial class Giris
    {
        [Key]
        [Column("id_gir")]
        public int IdGir { get; set; }
        [Column("kulad")]
        [StringLength(20)]
        public string? Kulad { get; set; }
        [Column("sifre")]
        [StringLength(20)]
        public string? Sifre { get; set; }
        [Column("mail")]
        [StringLength(40)]
        public string? Mail { get; set; }
        [Column("ad")]
        [StringLength(20)]
        public string? Ad { get; set; }
        [Column("soyad")]
        [StringLength(20)]
        public string? Soyad { get; set; }
        
    }
}
