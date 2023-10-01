using CodeChallenge.Common;
using CodeChallenge.Domain;
using CodeChallenge.Domain.Entities;

namespace CodeChallenge.Abstractions
{
    public interface IPlayerService
    {
        Task<Result<PlayerEntity>> AddPlayer(string sportId, Player player);

        Task<Result> RemovePlayer(string sportId, int playerId, string position);
    }
}
