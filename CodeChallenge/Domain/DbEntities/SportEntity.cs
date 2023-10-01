namespace CodeChallenge.Domain.Entities
{
    public class SportEntity
    {
        public string Id { get; set; }
        public string SportId { get; set; }
        public string Name { get; set; }
        public string Positions { get; set; } 
        public string Type => EntityType.Sport.ToString();
    }
}
