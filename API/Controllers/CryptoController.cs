using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Threading;
using Domain.model;
using Domain;
using System.Collections.Generic;
using MediatR;
using Application.Cryptos;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CryptoController : ControllerBase
  {
    private readonly IMediator _mediator;

    public CryptoController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<CryptoModel>>> List(CancellationToken ct)
    {

      return await _mediator.Send(new List.Query(), ct);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Crypto>> Get(Guid id)
    {
      return await _mediator.Send(new Get.Query { Id = id });
    }

  }
}