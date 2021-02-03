using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domain
{
  public class CryptoHistory
  {
    public Guid CryptoForeignKey { get; set; }

    public int Id { get; set; }

    [JsonIgnore]
    public Crypto Crypto { get; set; }

    public DateTime Date { get; set; }

    public double Open { get; set; }

    public double High { get; set; }
    public double Low { get; set; }

    public double Close { get; set; }

    public long Volume { get; set; }

    public long MarketCap { get; set; }
  }
}
