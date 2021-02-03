using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Domain.model;
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

  }
}