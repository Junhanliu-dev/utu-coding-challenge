using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using API.Controllers;
using Application.Cryptos;
using Domain.model;
using MediatR;
using Moq;
using Xunit;
using Xunit.Abstractions;
using Application.Cryptos;
using Microsoft.AspNetCore.Mvc;

namespace CryptoControllerTest
{
    public class CryptoControllerTestCase
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private Mock<IMediator> Mediator;
        public CryptoControllerTestCase(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            Mediator = new Mock<IMediator>();
        }
        
        [Fact]
        public async void Test_Get_Crypto_Success_Result()
        {

            CryptoController controller = new CryptoController(Mediator.Object);

            var result = await controller.List(new CancellationToken());

            Assert.IsType<ActionResult<List<CryptoModel>>>(result);
        }
    }
}