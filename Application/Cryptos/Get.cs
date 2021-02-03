using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Cryptos
{
  public class Get
  {
    public class Query : IRequest<Crypto>
    {
      public Guid Id { get; set; }
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
        var crypto = await _context.Cryptos.Include(c => c.History).FirstOrDefaultAsync(c => c.CryptoId == request.Id);

        return crypto;
      }

    }
  }
}