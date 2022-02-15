using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PaoloCattaneo.UrlShortner.Core.DAL
{
    public partial class UrlShortenerContext : DbContext
    {
        private string CONNECTION_STRING = "server=127.0.0.1;port=3306;user=urlshorteneruser;password=D3lta3;database=urlshortener";

        public UrlShortenerContext()
        {
        }

        public UrlShortenerContext(DbContextOptions<UrlShortenerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Url> Url { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(CONNECTION_STRING, ServerVersion.AutoDetect(CONNECTION_STRING));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Url>(entity =>
            {
                entity.ToTable("url");

                entity.HasComment("This table contains the shortened urls");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreationTime)
                    .HasColumnName("creation_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.ExpirationTime)
                    .HasColumnName("expiration_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .HasColumnName("key")
                    .HasColumnType("varchar(8)")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Url1)
                    .HasColumnName("url")
                    .HasColumnType("varchar(500)")
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
