namespace CodeChallenge.Domain.Entities
{
    public class DepthChartEntity
    {
        public Guid Id = Guid.NewGuid();
        public string Type => EntityType.DepthChart.ToString();
        public IEnumerable<int> RankedPlayerIds { get; set; }
        public string Position { get; set; }
        public string SportId { get; set; }

    }
}
