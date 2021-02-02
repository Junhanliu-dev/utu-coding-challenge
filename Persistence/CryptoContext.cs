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
      .HasForeignKey(h => h.CryptoForeignKey);

      SeedData(builder);

    }

    private void SeedData(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      using (StreamReader sr = new StreamReader("../Persistence/crypto_historical_data.csv"))
      {
        string currentLine;

        while ((currentLine = sr.ReadLine()) != null)
        {
          
        }

      }

    }
  }
}
