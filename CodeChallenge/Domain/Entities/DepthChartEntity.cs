namespace CodeChallenge.Domain.Entities
{
    public class DepthChartEntity
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public IEnumerable<Player> RankedPlayers { get; set; }
        public string Position { get; set; }
        public string SportId { get; set; }

    }
}
