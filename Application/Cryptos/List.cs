using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.helpler;
using Domain.model;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

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
                var viewModel = new List<CryptoModel>();

                var cryptos = await _context.Cryptos
                    .Include(c => c.History)
                    .ToListAsync(cancellationToken);

                if (cryptos == null)
                    throw new RestException(HttpStatusCode.NotFound, new {crypto = "No cryptos found"});

                foreach (var crypto in cryptos)
                {
                    var history = crypto.History;
                    var latestRecord = history.Count == 0 ? null : history[0];
                    var differenceIn24Hrs = 0d;
                    var differenceIn7Days = 0d;
                    var differenceInMonth = 0d;

                    if (latestRecord != null)
                    {
                        //sorting the history in desc
                        history.Sort((x, y) => y.Date.CompareTo(x.Date));
                        var latestDate = history[0].Date;

                        //Calculate price difference in past 24 hours
                        var past24Hrs = latestDate.AddHours(-24);
                        differenceIn24Hrs = PriceHelpler.CalPriceDifference(past24Hrs, latestDate, history);

                        //Calculate price difference in past 7 days
                        var past7Days = latestDate.AddDays(-7);
                        differenceIn7Days = PriceHelpler.CalPriceDifference(past7Days, latestDate, history);

                        //Calculate price difference in past month
                        var pastMonth = latestDate.AddMonths(-1);
                        differenceInMonth = PriceHelpler.CalPriceDifference(pastMonth, latestDate, history);
                    }

                    viewModel.Add(new CryptoModel
                    {
                        Id = crypto.CryptoId,
                        CurrencyName = crypto.CryptoName,
                        Price = latestRecord?.Open ?? 0d,
                        DifferenceIn24Hrs = differenceIn24Hrs,
                        DifferenceIn7Days = differenceIn7Days,
                        DifferenceInMonth = differenceInMonth,
                        Volume = latestRecord?.Volume ?? 0L,
                        MarketCap = latestRecord?.MarketCap ?? 0L
                    });
                }

                return viewModel;
            }
        }
    }
}