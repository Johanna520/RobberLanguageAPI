using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RobberLanguageAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobberLanguageAPI.Data
{
    public class RobberTranslationDBContext : DbContext
    {
   public RobberTranslationDBContext(DbContextOptions<RobberTranslationDBContext> options)
            : base(options)
        { 
        }

        public DbSet<Translation> Translations { get; set; }

        public static async Task CreateDb(IServiceProvider provider)
        {
            var context = provider.GetRequiredService<RobberTranslationDBContext>();

                await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }
    }
}
