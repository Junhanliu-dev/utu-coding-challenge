using System;

namespace Domain.model
{
    public class CryptoModel
    {
        public Guid Id { get; set; }
        public string CurrencyName { get; set; }

        public double Price { get; set; }

        public double DifferenceIn24Hrs { get; set; }
        public double DifferenceIn7Days { get; set; }
        public double DifferenceInMonth { get; set; }
        public long Volume { get; set; }

        public long MarketCap { get; set; }
    }
}