using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Cryptos;
using Domain;
using Domain.model;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            return await _mediator.Send(new Get.Query {Id = id});
        }

        [HttpGet("{id}/filter/{dateFilter?}")]
        public async Task<ActionResult<Crypto>> GetWithDate(Guid id, string dateFilter = "TwentyFourHours")
        {
            DateQueryFilter filter;

            try
            {
                filter = (DateQueryFilter) Enum.Parse(typeof(DateQueryFilter), dateFilter, true);
            }
            catch (ArgumentException e)
            {
                filter = DateQueryFilter.TwentyFourHours;
            }

            var query = new GetLatest.Query {Id = id, DateQueryFilter = filter};

            return await _mediator.Send(query);
        }
    }
}