using CodeChallenge.Domain.Entities;
using CodeChallenge.Repositories;

namespace CodeChallenge.Integration.Tests.Fakes
{
    public class FakeDepthChartRepository : IDepthChartRepository
    {
        public Task<DepthChartEntity> AddDepthChart(DepthChartEntity depthChartEntity)
        {
            throw new NotImplementedException();
        }

        public Task<DepthChartEntity> GetDepthChart(string sportId, string position)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DepthChartEntity>> GetDepthCharts(string sportId)
        {
            var depthCharts = new List<DepthChartEntity>()
            {
                new DepthChartEntity(){
                    Id = Guid.NewGuid(),
                    Position = "WR",
                    RankedPlayerIds = new int[]{2,4,5,3,1 },
                    SportId = "1"
                },
                new DepthChartEntity(){
                    Id = Guid.NewGuid(),
                    Position = "QB",
                    RankedPlayerIds = new int[]{7,11,9 },
                    SportId = "1"
                }
            };

            return Task.FromResult<IEnumerable<DepthChartEntity>>(depthCharts.AsEnumerable());
        }

        public Task UpdateDepthChart(DepthChartEntity depthChartEntity)
        {
            throw new NotImplementedException();
        }
    }
}
