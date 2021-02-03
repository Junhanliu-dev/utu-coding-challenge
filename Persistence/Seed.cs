using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using Domain;
using System.Globalization;

namespace Persistence
{
  public class Seed
  {
    public static void SeedData(CryptoContext context)
    {
      if (!context.Cryptos.Any())
      {
        List<Crypto> cryptoList = ParseFile("/Users/junhanliu/RiderProjects/Reader/crypto_historical_data.csv");

        context.Cryptos.AddRange(cryptoList);
        context.SaveChanges();

      }

    }

    private static List<Crypto> ParseFile(string filePath)
    {
      List<Crypto> seedData = new List<Crypto>();

      using (StreamReader sr = new StreamReader(filePath))
      {
        sr.ReadLine();
        string currentLine;

        while ((currentLine = sr.ReadLine()) != null)
        {
          List<string> lineInfo = StringHelper.SplitCSV(currentLine.Trim()).ToList();
          string currentCryptoName = lineInfo[0];

          if (seedData.Exists(c => c.CryptoName.Equals(currentCryptoName)))
          {
            Crypto crypto = seedData.Find(c => c.CryptoName.Equals(currentCryptoName));
            AddHistory(crypto, lineInfo);
          }
          else
          {
            Crypto crypto = new Crypto
            {
              CryptoName = currentCryptoName,
              CryptoId = Guid.NewGuid(),
              History = new List<CryptoHistory>(),
            };

            CryptoHistory history = ParseCryptoHistory(lineInfo);
            history.Crypto = crypto;
            history.CryptoForeignKey = crypto.CryptoId;
            crypto.History.Add(history);

            seedData.Add(crypto);

          }
        }
      }


      return seedData;
    }

    private static void AddHistory(Crypto crypto, List<string> lineInfo)
    {
      CryptoHistory history = ParseCryptoHistory(lineInfo);
      history.CryptoForeignKey = crypto.CryptoId;
      history.Crypto = crypto;

      crypto.History.Add(history);

    }
    private static CryptoHistory ParseCryptoHistory(List<string> lineInfo)
    {
      DateTime date = DateTime.Parse(lineInfo[1].Replace('"', ' ').Trim());
      double open = Double.Parse(lineInfo[2].Replace('"', ' ').Trim());
      double high = Double.Parse(lineInfo[3].Replace('"', ' ').Trim());
      double low = Double.Parse(lineInfo[4].Replace('"', ' ').Trim());
      double close = Double.Parse(lineInfo[5].Replace('"', ' ').Trim());
      long volume = long.Parse(lineInfo[6].Replace('"', ' ').Trim(), NumberStyles.Number);
      long marketCap = long.Parse(lineInfo[7].Replace('"', ' ').Trim(), NumberStyles.Number);


      return new CryptoHistory
      {
        Date = date,
        Open = open,
        High = high,
        Low = low,
        Close = close,
        Volume = volume,
        MarketCap = marketCap,
      };
    }

  }
}