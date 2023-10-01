using CodeChallenge.Common;
using CodeChallenge.Domain;

namespace CodeChallenge.Abstractions
{
    public interface IDepthChartService
    {
        Task<Result> AddPlayer(string sportId, Player player);

        Task<Result> RemovePlayer(string sportId, int playerId, string position);

        Task<Result<IEnumerable<DepthChartResponse>>> GetDepthCharts(string sportId);

        Task<Result<int[]>> GetPlayersBehindThePlayerInDepthChart(string sportId, string position, int playerId);
    }
}
