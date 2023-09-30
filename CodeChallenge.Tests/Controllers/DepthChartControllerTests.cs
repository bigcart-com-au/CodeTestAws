using CodeChallenge.Abstractions;
using CodeChallenge.Common;
using CodeChallenge.Controllers;
using CodeChallenge.Domain;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Net;

namespace CodeChallenge.Tests.Controllers
{
    public class DepthChartControllerTests
    {
        private readonly DepthChartController _controller;
        private IDepthChartService _depthChartService;

        public DepthChartControllerTests()
        {
            _depthChartService = Substitute.For<IDepthChartService>();
            _controller = new DepthChartController(_depthChartService);
        }

        [Fact]
        public async Task GetDepthCharts_WhenNoQueryprovided_ThenReturnAllDepthChartsForSport()
        {
            // Arrange
            var sportId = "1";
            var emptyQuery = new QueryPlayersBehindThePlayerInDepthChart();
            Result<IEnumerable<DepthChartResponse>> successResult = GenerateSuccessResult();
            _depthChartService
                .GetDepthCharts(sportId)
                .Returns(Task.FromResult(successResult));

            // Act
            var httpResponse = (OkObjectResult)await _controller.GetDepthCharts(sportId, emptyQuery);

            // Assert
            httpResponse.StatusCode.Should().Be((int)HttpStatusCode.OK);
            ((IEnumerable<DepthChartResponse>)httpResponse.Value).Count().Should().Be(2);
        }

        [Fact]
        public async Task GetDepthCharts_WhenValidQueryprovided_ThenReturnPlayersBehindThePlayerInQuery()
        {
            // Arrange
            var sportId = "1";
            var query = new QueryPlayersBehindThePlayerInDepthChart { 
                 PlayerId = 3,
                 Position = "WR"
            };
            _depthChartService
                .GetPlayersBehindThePlayerInDepthChart(sportId, query.Position, query.PlayerId.Value)
                .Returns(Task.FromResult(Result.Ok(new int[] { 4,1 })));

            // Act
            var httpResponse = (OkObjectResult)await _controller.GetDepthCharts(sportId, query);

            // Assert
            httpResponse.StatusCode.Should().Be((int)HttpStatusCode.OK);
            ((int[])httpResponse.Value).Should().BeEquivalentTo(new int[] { 4,1 });
        }

        [Fact]
        public async Task GetDepthCharts_WhenInvalidPlayerIsProvided_ThenReturnBadRequest()
        {
            // Arrange
            var sportId = "1";
            var query = new QueryPlayersBehindThePlayerInDepthChart
            {
                PlayerId = 23,
                Position = "WR"
            };
            _depthChartService
                .GetPlayersBehindThePlayerInDepthChart(sportId, query.Position, query.PlayerId.Value)
                .Returns(Task.FromResult(Result.Fail<int[]>("Player not in position")));

            // Act
            var httpResponse = (ObjectResult)await _controller.GetDepthCharts(sportId, query);

            // Assert
            httpResponse.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        private static Result<IEnumerable<DepthChartResponse>> GenerateSuccessResult()
        {
            return Result.Ok(new List<DepthChartResponse> {
                new DepthChartResponse
                {
                    Position = "WR",
                    RankedPlayerIds = new int[]{ 3,4,1 }
                },
                new DepthChartResponse
                {
                    Position = "KR",
                    RankedPlayerIds = new int[]{ 2,1 }
                }
            }.AsEnumerable());
        }
    }
}
