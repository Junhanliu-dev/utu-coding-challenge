using System.IO;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Persistence
{
  public class CryptoContext : DbContext
  {
    public DbSet<Crypto> Cryptos { get; set; }
    public DbSet<CryptoHistory> CryptoHistory { get; set; }
    public CryptoContext(DbContextOptions options) : base(options)
    {

    }

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