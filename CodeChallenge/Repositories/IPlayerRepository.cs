using CodeChallenge.Domain.Entities;

namespace CodeChallenge.Repositories
{
    public interface IPlayerRepository
    {
        Task<PlayerEntity> AddPlayer(PlayerEntity player);

        Task<PlayerEntity> GetPlayer(string sportId, int playerId);
    }
}
