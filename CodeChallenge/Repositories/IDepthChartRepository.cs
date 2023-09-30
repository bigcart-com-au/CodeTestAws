using CodeChallenge.Domain.Entities;

namespace CodeChallenge.Repositories
{
    public interface IDepthChartRepository
    {
        Task<DepthChartEntity> GetDepthChart(string sportId, string position);

        Task<IEnumerable<DepthChartEntity>> GetDepthCharts(string sportId);

        Task<DepthChartEntity> AddDepthChart(DepthChartEntity depthChartEntity);

        Task UpdateDepthChart(DepthChartEntity depthChartEntity);
    }
}
