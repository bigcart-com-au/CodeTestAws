using CodeChallenge.Domain.Entities;

namespace CodeChallenge.Repositories
{
    public interface ISportRepository
    {
        Task AddSport(SportEntity sport);
    }
}
