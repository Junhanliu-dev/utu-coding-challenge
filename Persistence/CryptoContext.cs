using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class CryptoContext : DbContext
    {
        public CryptoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Crypto> Cryptos { get; set; }
        public DbSet<CryptoHistory> CryptoHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CryptoHistory>()
                .HasOne(c => c.Crypto)
                .WithMany(h => h.History)
                .HasForeignKey(h => h.CryptoForeignKey)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}