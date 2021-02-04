using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static void SeedData(CryptoContext context)
        {
            if (!context.Cryptos.Any())
            {
                var cryptoList = ParseFile("/Users/junhanliu/RiderProjects/Reader/crypto_historical_data.csv");

                context.Cryptos.AddRange(cryptoList);
                context.SaveChanges();
            }
        }

        private static List<Crypto> ParseFile(string filePath)
        {
            var seedData = new List<Crypto>();

            using (var sr = new StreamReader(filePath))
            {
                sr.ReadLine();
                string currentLine;

                while ((currentLine = sr.ReadLine()) != null)
                {
                    var lineInfo = currentLine.Trim().SplitCSV().ToList();
                    var currentCryptoName = lineInfo[0];

                    if (seedData.Exists(c => c.CryptoName.Equals(currentCryptoName)))
                    {
                        var crypto = seedData.Find(c => c.CryptoName.Equals(currentCryptoName));
                        AddHistory(crypto, lineInfo);
                    }
                    else
                    {
                        var crypto = new Crypto
                        {
                            CryptoName = currentCryptoName,
                            CryptoId = Guid.NewGuid(),
                            History = new List<CryptoHistory>()
                        };

                        var history = ParseCryptoHistory(lineInfo);
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
            var history = ParseCryptoHistory(lineInfo);
            history.CryptoForeignKey = crypto.CryptoId;
            history.Crypto = crypto;

            crypto.History.Add(history);
        }

        private static CryptoHistory ParseCryptoHistory(List<string> lineInfo)
        {
            var date = DateTime.Parse(lineInfo[1].Replace('"', ' ').Trim());
            var open = double.Parse(lineInfo[2].Replace('"', ' ').Trim());
            var high = double.Parse(lineInfo[3].Replace('"', ' ').Trim());
            var low = double.Parse(lineInfo[4].Replace('"', ' ').Trim());
            var close = double.Parse(lineInfo[5].Replace('"', ' ').Trim());
            var volume = long.Parse(lineInfo[6].Replace('"', ' ').Trim(), NumberStyles.Number);
            var marketCap = long.Parse(lineInfo[7].Replace('"', ' ').Trim(), NumberStyles.Number);


            return new CryptoHistory
            {
                Date = date,
                Open = open,
                High = high,
                Low = low,
                Close = close,
                Volume = volume,
                MarketCap = marketCap
            };
        }
    }
}