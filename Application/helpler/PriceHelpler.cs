using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Application.helpler
{
    public class PriceHelpler
    {
        public static double CalPriceDifference(DateTime dateFrom, DateTime dateTo, List<CryptoHistory> history)
        {
            var historyClosePriceInDate = history
                .Where(h => h.Date >= dateFrom && h.Date <= dateTo)
                .OrderByDescending(h => h.Date)
                .Select(h => h.Close)
                .ToList();

            var newNumber = historyClosePriceInDate[0];
            var originalNumber = historyClosePriceInDate.Last();

            var difference = (newNumber - originalNumber) / originalNumber * 100;
            // Console.WriteLine(Math.Round(difference, 2));
            return Math.Round(difference, 2);
        }
    }
}