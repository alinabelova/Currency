using GPN.CR.TEST.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GPN.CR.TEST.Data.EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyDaily> CurrencyDaily { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}