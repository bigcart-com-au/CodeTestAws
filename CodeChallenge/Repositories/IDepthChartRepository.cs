using CodeChallenge.Domain.Entities;

namespace CodeChallenge.Repositories
{
    public interface IDepthChartRepository
    {
        Task<DepthChartEntity> GetDepthChart(string position);

        Task<IEnumerable<DepthChartEntity>> GetDepthCharts(int sportId);
    }
}
