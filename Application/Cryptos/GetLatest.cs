using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Cryptos
{
  public class GetLatest
  {
    public class Query : IRequest<Crypto>
    {
      public Guid Id { get; set; }

    }
  }
}