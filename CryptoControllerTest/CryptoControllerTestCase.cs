using System;
using System.Collections.Generic;
using System.Threading;
using API.Controllers;
using Application.Errors;
using Domain.model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace CryptoControllerTest
{
    public class CryptoControllerTestCase
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly CryptoController _controller;
        private readonly Mock<IMediator> Mediator;

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