using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Practica02NARA.Models
{
    public partial class Practica02NARABDContext : DbContext
    {
        public Practica02NARABDContext()
        {
        }

        public Practica02NARABDContext(DbContextOptions<Practica02NARABDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Director> Directores { get; set; } = null!;
        public virtual DbSet<Pelicula> Peliculas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-NFDMETJ;Initial Catalog=Practica02NARABD;Integrated Security=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Director>(entity =>
            {
                entity.HasKey(e => e.IdDirector)
                    .HasName("PK__Director__FE31570D609C0C13");

                entity.Property(e => e.NombreDirector)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pelicula>(entity =>
            {
                entity.Property(e => e.AñoPublicada)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Genero)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ImagenPelicula).HasColumnType("image");

                entity.Property(e => e.NombrePelicula)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.DirectorPeliculaNavigation)
                    .WithMany(p => p.Peliculas)
                    .HasForeignKey(d => d.DirectorPelicula)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Peliculas__Direc__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
