using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Cryptos
{
    public enum DateQueryFilter
    {
        SevenDays = 1,
        TwentyFourHours = 2,
        AMonth = 3
    }

    public class GetLatest
    {
        public class Query : IRequest<Crypto>
        {
            public Guid Id { get; set; }
            public DateQueryFilter DateQueryFilter { get; set; }
        }

        public class Handler : IRequestHandler<Query, Crypto>
        {
            private readonly CryptoContext _context;

            public Handler(CryptoContext context)
            {
                _context = context;
            }

            public async Task<Crypto> Handle(Query request, CancellationToken ct)
            {
                var crypto = await _context.Cryptos.FirstOrDefaultAsync(c => c.CryptoId == request.Id, ct);

                if (crypto == null)
                    throw new RestException(HttpStatusCode.NotFound,
                        new {crypto = "Could not find crypto information"});

                var latestRecord = await _context.CryptoHistory
                    .Where(h => h.Crypto.CryptoId == request.Id)
                    .OrderByDescending(h => h.Date)
                    .FirstOrDefaultAsync(ct);

                List<CryptoHistory> history;
                DateTime date;

                switch (request.DateQueryFilter)
                {
                    case DateQueryFilter.TwentyFourHours:
                        date = latestRecord.Date.AddHours(-24);
                        break;

                    case DateQueryFilter.SevenDays:
                        date = latestRecord.Date.AddDays(-7);

                        break;
                    case DateQueryFilter.AMonth:
                        date = latestRecord.Date.AddMonths(-1);
                        break;
                    default:
                        history = new List<CryptoHistory>();
                        date = latestRecord.Date;
                        break;
                }

                history = await _context.CryptoHistory.Where(h =>
                    h.Crypto.CryptoId == request.Id && h.Date >= date && h.Date <= latestRecord.Date).ToListAsync(ct);

                crypto.History = history;

                return crypto;
            }
        }
    }
}