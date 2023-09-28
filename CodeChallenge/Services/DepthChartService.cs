using CodeChallenge.Abstractions;
using CodeChallenge.Domain;

namespace CodeChallenge.Services
{
    public class DepthChartService : IDepthChartService
    {
        Task<Result> IDepthChartService.AddPlayer(int sportId, Player player)
        {
            throw new NotImplementedException();
        }

        Result<DepthChart> IDepthChartService.GetDepthChart(string Position)
        {
            throw new NotImplementedException();
        }

        Result<IEnumerable<DepthChart>> IDepthChartService.GetDepthCharts()
        {
            throw new NotImplementedException();
        }
    }
}
