using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrakeadorWeb.Models;

namespace TrakeadorWeb.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            try
            {
                // Ensure database is created and apply pending migrations
                await context.Database.EnsureCreatedAsync();
                
                // Check if migrations are needed
                var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                // If migrations fail, continue with seeding as the database might already be up to date
                Console.WriteLine($"Migration warning: {ex.Message}");
            }

            // Seed Casas de Apostas
            if (!await context.CasasDeApostas.AnyAsync())
            {
                var casasDeApostas = new List<CasaDeApostas>
                {
                    new() {
                        Nome = "Esportiva",
                        Descricao = "Casa de apostas esportivas com foco no mercado brasileiro",
                        UrlBase = "https://go.aff.esportiva.bet/",
                        Ativo = true,
                        DataCriacao = DateTime.Now
                    },
                    new() {
                        Nome = "Novibet",
                        Descricao = "Plataforma internacional de apostas esportivas",
                        UrlBase = "https://www.novibet.bet.br/",
                        Ativo = true,
                        DataCriacao = DateTime.Now
                    },
                    new() {
                        Nome = "BetMGM",
                        Descricao = "Casa de apostas com sistema de cupons",
                        UrlBase = "https://www.betmgm.bet.br/",
                        Ativo = true,
                        DataCriacao = DateTime.Now
                    }
                };

                await context.CasasDeApostas.AddRangeAsync(casasDeApostas);
                await context.SaveChangesAsync();
            }

            // Seed Experts
            if (!await context.Experts.AnyAsync())
            {
                var experts = new List<Expert>
                {
                    new() {
                        Nome = "João Silva",
                        Descricao = "Especialista em apostas esportivas com foco em futebol brasileiro",
                        Ativo = true,
                        DataCriacao = DateTime.Now
                    },
                    new() {
                        Nome = "Maria Santos",
                        Descricao = "Analista de mercados internacionais e apostas ao vivo",
                        Ativo = true,
                        DataCriacao = DateTime.Now
                    }
                };

                await context.Experts.AddRangeAsync(experts);
                await context.SaveChangesAsync();
            }

            // Seed Master User
            if (userManager.Users.Count() == 0)
            {
                var masterUser = new IdentityUser
                {
                    UserName = "admin@trakeador.com",
                    Email = "admin@trakeador.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(masterUser, "Admin@123");
                
                if (result.Succeeded)
                {
                    // Opcional: adicionar claims ou roles se necessário no futuro
                }
            }
        }
    }
}