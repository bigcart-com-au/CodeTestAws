using CodeChallenge.Domain.Entities;

namespace CodeChallenge.Repositories
{
    public interface ISportRepository
    {
        Task<SportEntity> GetSport(string sportId);
    }
}
