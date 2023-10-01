using CodeChallenge.Abstractions;
using CodeChallenge.Common;
using CodeChallenge.Domain;
using CodeChallenge.Domain.Entities;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class DepthChartService : IDepthChartService
    {
        private readonly IDepthChartRepository _depthChartRepository;
        private readonly ILogger<DepthChartService> _logger;

        public DepthChartService(
            IDepthChartRepository depthChartRepository,
            ILogger<DepthChartService> logger)
        {
            _depthChartRepository = depthChartRepository;
            _logger = logger;
        }

        public async Task<Result> AddPlayer(string sportId, Player player)
        {
            var depthChart = await _depthChartRepository.GetDepthChart(sportId, player.Position);

            if (depthChart == null)
            {
                await CreateDepthChart(sportId, player);
            }
            else
            {
                await UpdateDepthChart(sportId, player, depthChart);
            }

            return Result.Ok();
        }

        private async Task UpdateDepthChart(string sportId, Player player, DepthChartEntity depthChart)
        {
            var currentPlayerRanking = depthChart.RankedPlayerIds;

            var updatedPlayerRanking = PlayerPositions.Rank(player.PositionDepth, player.PlayerId, currentPlayerRanking);

            var depthChartEntity = new DepthChartEntity
            {
                Id = depthChart.Id,
                Position = player.Position,
                SportId = sportId,
                RankedPlayerIds = updatedPlayerRanking
            };

            await _depthChartRepository.UpdateDepthChart(depthChartEntity);
        }

        private async Task CreateDepthChart(string sportId, Player player)
        {
            var rankedPlayers = new List<int>
            {
                player.PlayerId
            };

            var depthChartEntity = new DepthChartEntity
            {
                Position = player.Position,
                SportId = sportId,
                RankedPlayerIds = rankedPlayers
            };

            await _depthChartRepository.AddDepthChart(depthChartEntity);
        }

        public async Task<Result<IEnumerable<DepthChartResponse>>> GetDepthCharts(string sportId)
        {
            var result = await _depthChartRepository.GetDepthCharts(sportId);

            var depthChartsResponse = result.Select(e => new DepthChartResponse
            {
                Position = e.Position,
                RankedPlayerIds = e.RankedPlayerIds.ToArray()
            });

            return Result.Ok(depthChartsResponse);
        }

        public async Task<Result<int[]>> GetPlayersBehindThePlayerInDepthChart(string sportId, string position, int playerId)
        {
            var depthChart = await _depthChartRepository.GetDepthChart(sportId, position);

            var playerRanking = depthChart.RankedPlayerIds.ToList();
            var indexOfPlayer = playerRanking.FindIndex(p => p == playerId);

            if (indexOfPlayer == -1)
            {
                return Result.Fail<int[]>("Player not found in position");
            }

            var playersBehindThePlayerInDepthChart = playerRanking.GetRange(indexOfPlayer+1, playerRanking.Count - (indexOfPlayer+1)).ToArray();

            return Result.Ok(playersBehindThePlayerInDepthChart);
        }

        public async Task<Result> RemovePlayer(string sportId, int playerId, string position)
        {
            var depthChart = await _depthChartRepository.GetDepthChart(sportId, position);
            var playerRanking = depthChart.RankedPlayerIds.ToList();

            if (!playerRanking.Contains(playerId))
            {
                return Result.Fail("Player not in position");
            }

            var indexOfPlayerRankToRemove = playerRanking.IndexOf(playerId);
            var updatedPlayerRanking = playerRanking.Where((item, index) => index != indexOfPlayerRankToRemove).ToList();

            var depthChartEntity = new DepthChartEntity
            {
                Id = depthChart.Id,
                Position = position,
                SportId = sportId,
                RankedPlayerIds = updatedPlayerRanking
            };

            await _depthChartRepository.UpdateDepthChart(depthChartEntity);

            return Result.Ok();
        }
    }
}
