using System;
using System.Collections.Generic;
using Domain;
using System.Linq;

namespace Application.helpler
{
  public class PriceHelpler
  {
    public static double CalPriceDifference(DateTime dateFrom, DateTime dateTo, List<CryptoHistory> history)
    {

      List<double> historyClosePriceInDate = history
                                          .Where(h => (h.Date >= dateFrom) && (h.Date <= dateTo))
                                          .OrderByDescending(h => h.Date)
                                          .Select(h => h.Close)
                                          .ToList();

      double newNumber = historyClosePriceInDate[0];
      double originalNumber = historyClosePriceInDate.Last();

      double difference = ((newNumber - originalNumber) / originalNumber) * 100;
      // Console.WriteLine(Math.Round(difference, 2));
      return Math.Round(difference, 2);
    }
  }
}