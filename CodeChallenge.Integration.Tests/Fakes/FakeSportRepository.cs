using CodeChallenge.Domain.Entities;
using CodeChallenge.Repositories;

namespace CodeChallenge.Integration.Tests.Fakes
{
    public class FakeSportRepository : ISportRepository
    {
        public Task<SportEntity> GetSport(string sportId)
        {
            return Task.FromResult(new SportEntity { 
                Id = "1",
                Name = "NFL",
                Positions = new string[] { "QB","WR","RB" },
                SportId = "1"
            });
        }
    }
}
