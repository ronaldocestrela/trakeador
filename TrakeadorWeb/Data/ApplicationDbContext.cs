using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TrakeadorWeb.Models;

namespace TrakeadorWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Expert> Experts { get; set; }
        public DbSet<CasaDeApostas> CasasDeApostas { get; set; }
        public DbSet<ExpertCasaApostasAfiliado> ExpertCasaApostasAfiliados { get; set; }
        public DbSet<Canal> Canais { get; set; }
        public DbSet<Destino> Destinos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuração da entidade Expert
            builder.Entity<Expert>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descricao).HasMaxLength(500);
                entity.HasIndex(e => e.Nome).IsUnique();
            });

            // Configuração da entidade CasaDeApostas
            builder.Entity<CasaDeApostas>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descricao).HasMaxLength(500);
                entity.Property(e => e.UrlBase).HasMaxLength(200);
                entity.HasIndex(e => e.Nome).IsUnique();
            });

            // Configuração da entidade ExpertCasaApostasAfiliado
            builder.Entity<ExpertCasaApostasAfiliado>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CodigoAfiliado).IsRequired().HasMaxLength(500);
                entity.Property(e => e.ParametrosAdicionais).HasMaxLength(1000);

                // Relacionamentos
                entity.HasOne(e => e.Expert)
                    .WithMany(e => e.CasasDeApostas)
                    .HasForeignKey(e => e.ExpertId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.CasaDeApostas)
                    .WithMany(c => c.Experts)
                    .HasForeignKey(e => e.CasaDeApostasId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Índice único para evitar duplicação de expert + casa de apostas
                entity.HasIndex(e => new { e.ExpertId, e.CasaDeApostasId }).IsUnique();
            });

            // Configuração da entidade Canal
            builder.Entity<Canal>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Nome).IsUnique();
            });

            // Configuração da entidade Destino
            builder.Entity<Destino>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);

                // Relacionamento com Canal
                entity.HasOne(e => e.Canal)
                    .WithMany(c => c.Destinos)
                    .HasForeignKey(e => e.CanalId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Índice único para evitar duplicação de nome dentro do mesmo canal
                entity.HasIndex(e => new { e.Nome, e.CanalId }).IsUnique();
            });
        }
    }
}