using CodeChallenge.Common;
using CodeChallenge.Domain;
using CodeChallenge.Domain.Entities;
using CodeChallenge.Repositories;
using CodeChallenge.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace CodeChallenge.Tests.Services
{
    public class DepthChartServiceTests
    {
        private readonly DepthChartService _service;
        private IDepthChartRepository _depthChartRepository;
        private ISportRepository _sportChartRepository;

        public DepthChartServiceTests()
        {
            _depthChartRepository = Substitute.For<IDepthChartRepository>();
            _sportChartRepository = Substitute.For<ISportRepository>();
            var logger = Substitute.For<ILogger<DepthChartService>>();

            _service = new DepthChartService(_depthChartRepository, _sportChartRepository,  logger);
        }

        [Fact]
        public async Task GetDepthCharts_WhenSportIdIsNotValid_ReturnFailSportNotValid()
        {
            // Arrange
            var sportsId = "1";
            _sportChartRepository
                .GetSport(sportsId)
                .Returns(Task.FromResult<SportEntity>(null));

            // Act
            var result = await _service.GetDepthCharts(sportsId);

            // Assert
            result.Success.Should().Be(false);
        }

        [Fact]
        public async Task AddPlayer_WhenDepthChartIsEmpty_CreateDepthChartAndAddPlayerToFirstPosition()
        {
            // Arrange
            var sportsId = "1";
            var player = new Player
            {
                Name = "Bob",
                PlayerId = 1,
                Position = "WR",
                PositionDepth = 4
            };
            _depthChartRepository
                .GetDepthChart(sportsId, player.Position)
                .Returns(Task.FromResult((DepthChartEntity)null));
            _sportChartRepository
                .GetSport(sportsId)
                .Returns(Task.FromResult(new SportEntity { SportId = "1" }));

            // Act
            var result = await _service.AddPlayer(sportsId, player);

            // Assert
            result.Success.Should().Be(true);
            await _depthChartRepository
                .Received()
                .AddDepthChart(Arg.Is<DepthChartEntity>(entity => entity.RankedPlayerIds.Single() == 1));
        }

        [Fact]
        public async Task AddPlayer_WhenDepthChartIsNotEmpty_UpdateDepthChartWithPlayerPosition()
        {
            // Arrange
            var sportsId = "1";
            var player = new Player
            {
                Name = "Bob",
                PlayerId = 1,
                Position = "WR",
                PositionDepth = 4
            };
            var depthChart = new DepthChartEntity
            {
                RankedPlayerIds = new List<int> { 5, 2, 3, 4, 7, 9, 8 }
            };
            _depthChartRepository
                .GetDepthChart(sportsId, player.Position)
                .Returns(Task.FromResult(depthChart));
            _sportChartRepository
                .GetSport(sportsId)
                .Returns(Task.FromResult(new SportEntity { SportId = "1" }));

            // Act
            var result = await _service.AddPlayer(sportsId, player);

            // Assert
            result.Success.Should().Be(true);
            await _depthChartRepository
                .Received()
                .UpdateDepthChart(Arg.Is<DepthChartEntity>(entity => entity.RankedPlayerIds.SequenceEqual(new List<int> { 5, 2, 3, 4, 1, 7, 9, 8 })));
        }
    }
}
