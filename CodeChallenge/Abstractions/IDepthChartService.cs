using CodeChallenge.Domain;

namespace CodeChallenge.Abstractions
{
    public interface IDepthChartService
    {
        Task<Result> AddPlayer(int sportId, Player player);

        Result<IEnumerable<DepthChart>> GetDepthCharts();

        Result<DepthChart> GetDepthChart(string position);

        Result<DepthChart> GetDepthChartRemainingPlayer(string position, string playerName);
    }
}
