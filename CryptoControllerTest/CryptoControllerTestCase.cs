using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
using Application.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CryptoControllerTest
{
    public class CryptoControllerTestCase
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private Mock<IMediator> Mediator;
        private CryptoController _controller;
        public CryptoControllerTestCase(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            Mediator = new Mock<IMediator>();
            _controller = new CryptoController(Mediator.Object);   
        }
        
        [Fact]
        public async void Test_Get_Crypto_Success_Result()
        {

            var result = await _controller.List(new CancellationToken());

            Assert.IsType<ActionResult<List<CryptoModel>>>(result);
        }
        
        [Fact]
        public async void Test_Get_Crypto_Result()
        {
            var result = _controller.Get(It.IsAny<Guid>());

            Assert.IsType<ActionResult<RestException>>(result);
        }
    }
}