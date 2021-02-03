using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.model;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Linq;

namespace Application.Cryptos
{
  public class List
  {
    public class Query : IRequest<List<CryptoModel>>
    {
    }


    public class Handler : IRequestHandler<Query, List<CryptoModel>>
    {
      private readonly CryptoContext _context;

      public Handler(CryptoContext context)
      {
        _context = context;
      }
      public async Task<List<CryptoModel>> Handle(Query request, CancellationToken cancellationToken)
      {
        List<CryptoModel> viewModel = new List<CryptoModel>();

        List<Crypto> cryptos = await _context.Cryptos
                                .Include(c => c.History)
                                .ToListAsync();

        foreach (Crypto crypto in cryptos)
        {
          List<CryptoHistory> history = crypto.History;
          CryptoHistory latestRecord = history.Count == 0 ? null : history[0];
          double differenceIn24Hrs = 0d;
          double differenceIn7Days = 0d;
          double differenceInMonth = 0d;

          if (latestRecord != null)
          {
            //sorting the history in desc
            history.Sort((x, y) => y.Date.CompareTo(x.Date));
            DateTime latestDate = history[0].Date;

            //Calculate price difference in past 24 hours
            DateTime past24Hrs = latestDate.AddHours(-24);
            differenceIn24Hrs = CalPriceDifference(past24Hrs, latestDate, history);

            //Calculate price difference in past 7 days
            DateTime past7Days = latestDate.AddDays(-7);
            differenceIn7Days = CalPriceDifference(past7Days, latestDate, history);

            //Calculate price difference in past month
            DateTime pastMonth = latestDate.AddMonths(-1);
            differenceInMonth = CalPriceDifference(pastMonth, latestDate, history);
          }

          viewModel.Add(new CryptoModel
          {
            Id = crypto.CryptoId,
            CurrencyName = crypto.CryptoName,
            Price = latestRecord == null ? 0d : latestRecord.Open,
            DifferenceIn24Hrs = differenceIn24Hrs,
            DifferenceIn7Days = differenceIn7Days,
            DifferenceInMonth = differenceInMonth,
            Volume = latestRecord == null ? 0L : latestRecord.Volume,
            MarketCap = latestRecord == null ? 0L : latestRecord.MarketCap
          });
        }

        return viewModel;
      }

      private double CalPriceDifference(DateTime dateFrom, DateTime dateTo, List<CryptoHistory> history)
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
}