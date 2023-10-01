using CodeChallenge.Abstractions;
using CodeChallenge.Common;
using CodeChallenge.Domain;
using CodeChallenge.Domain.Entities;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IDepthChartService _depthChartService;
        private readonly ISportRepository _sportRepository;
        private readonly ILogger<PlayerService> _logger;

        public PlayerService(
            IPlayerRepository playerRepository,
            IDepthChartService depthChartService,
            ISportRepository sportRepository,
            ILogger<PlayerService> logger)
        {
            _playerRepository = playerRepository;
            _depthChartService = depthChartService;
            _sportRepository = sportRepository;
            _logger = logger;
        }

        public async Task<Result<PlayerEntity>> AddPlayer(string sportId, Player player)
        {
            var sport = await _sportRepository.GetSport(sportId);
            if (sport == null) {
                return Result.Fail<PlayerEntity>("Sport doesn't exist");
            }

            var existingPlayer = await _playerRepository.GetPlayer(sportId, player.PlayerId);

            if (existingPlayer != null) {
                return Result.Fail<PlayerEntity>("Player already exists");
            }

            var playerEntity = new PlayerEntity
            {
                Name = player.Name,
                PlayerId = player.PlayerId,
                SportId = sportId,
            };

            var persistedPlayerEntity = await _playerRepository.AddPlayer(playerEntity);

            if (persistedPlayerEntity == null) {
                return Result.Fail<PlayerEntity>("Creating player failed");
            }

            var depthChartModificationResult = await _depthChartService.AddPlayer(sportId, player);

            if (!depthChartModificationResult.Success)
            {
                _logger.LogError("Adding player to depthchart failed !!");
            }

            return Result.Ok(playerEntity);
        }

     
        public async Task<Result> RemovePlayer(string sportId, int playerId, string position)
        {
            var sport = await _sportRepository.GetSport(sportId);
            if (sport == null)
            {
                return Result.Fail<PlayerEntity>("Sport doesn't exist");
            }

            var existingPlayer = await _playerRepository.GetPlayer(sportId, playerId);
            if (existingPlayer == null)
            {
                return Result.Fail("Player doesn't exist");
            }

            var result = await _depthChartService.RemovePlayer(sportId, playerId, position);
            if (result.IsFailure) {
                return Result.Fail(result.Error);
            }

            return Result.Ok();
        }
    }
}
