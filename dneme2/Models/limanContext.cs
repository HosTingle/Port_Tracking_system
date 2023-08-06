using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace dneme2.Models
{
    public partial class limanContext : DbContext
    {
        public limanContext()
        {
        }

        public limanContext(DbContextOptions<limanContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bolge> Bolges { get; set; } = null!;
        public virtual DbSet<GemiBilgi> GemiBilgis { get; set; } = null!;
        public virtual DbSet<Giris> Girises { get; set; } = null!;
        public virtual DbSet<LimanBilgi> LimanBilgis { get; set; } = null!;
        public virtual DbSet<Personel> Personels { get; set; } = null!;
        public virtual DbSet<Sirket> Sirkets { get; set; } = null!;
        public virtual DbSet<Tero> Teros { get; set; } = null!;
        public virtual DbSet<YükBilgi> YükBilgis { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-DNUIALQ\\SQLKOD;Initial Catalog=liman;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Encrypt=False;Trust Server Certificate=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bolge>(entity =>
            {
                entity.Property(e => e.TerritoryDes).IsFixedLength();

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Bolges)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bolge_Tero");
            });

            modelBuilder.Entity<GemiBilgi>(entity =>
            {
                entity.HasKey(e => e.IdGembilgi)
                    .HasName("PK_gemibilgi");

                entity.Property(e => e.Bulkon).IsFixedLength();

                entity.Property(e => e.GemiAd).IsFixedLength();

                entity.Property(e => e.Gitkon).IsFixedLength();

                entity.HasOne(d => d.GemiPerNavigation)
                    .WithMany(p => p.GemiBilgis)
                    .HasForeignKey(d => d.GemiPer)
                    .HasConstraintName("FK_Gemi_bilgi_Personel");

                entity.HasOne(d => d.IdSirketlerNavigation)
                    .WithMany(p => p.GemiBilgis)
                    .HasForeignKey(d => d.IdSirketler)
                    .HasConstraintName("FK_Gemi_bilgi_Sirket1");

                entity.HasOne(d => d.IdYüklerNavigation)
                    .WithMany(p => p.GemiBilgis)
                    .HasForeignKey(d => d.IdYükler)
                    .HasConstraintName("FK_Gemi_bilgi_Sirket");
            });

            modelBuilder.Entity<Giris>(entity =>
            {
                entity.HasKey(e => e.IdGir)
                    .HasName("PK_giris");

                entity.Property(e => e.Ad).IsFixedLength();

                entity.Property(e => e.Kulad).IsFixedLength();

                entity.Property(e => e.Mail).IsFixedLength();

                entity.Property(e => e.Sifre).IsFixedLength();

                entity.Property(e => e.Soyad).IsFixedLength();
            });

            modelBuilder.Entity<LimanBilgi>(entity =>
            {
                entity.HasKey(e => e.IdLima)
                    .HasName("PK_Limanbilgi");

                entity.Property(e => e.Bulseh).IsFixedLength();

                entity.Property(e => e.Bulülke).IsFixedLength();

                entity.Property(e => e.Limanad).IsFixedLength();

                entity.HasOne(d => d.IdGemilerNavigation)
                    .WithMany(p => p.LimanBilgis)
                    .HasForeignKey(d => d.IdGemiler)
                    .HasConstraintName("FK_Liman_bilgi_Gemi_bilgi");
            });

            modelBuilder.Entity<Personel>(entity =>
            {
                entity.Property(e => e.Ad).IsFixedLength();

                entity.Property(e => e.Cinsiyet).IsFixedLength();

                entity.Property(e => e.Soyad).IsFixedLength();
            });

            modelBuilder.Entity<Sirket>(entity =>
            {
                entity.HasKey(e => e.IdSir)
                    .HasName("PK_sirket");

                entity.Property(e => e.Sirketisim).IsFixedLength();

                entity.Property(e => e.SirketÜlke).IsFixedLength();
            });

            modelBuilder.Entity<Tero>(entity =>
            {
                entity.Property(e => e.Region).IsFixedLength();
            });

            modelBuilder.Entity<YükBilgi>(entity =>
            {
                entity.HasKey(e => e.IdYükbil)
                    .HasName("PK_yükbilgi");

                entity.Property(e => e.YukTur).IsFixedLength();

                entity.Property(e => e.YükGüven).IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
