namespace CodeChallenge.Domain.Entities
{
    public class PlayerEntity
    {
        public string Id => Guid.NewGuid().ToString();  

        public string Type => EntityType.Player.ToString();

        public string Name { get; set; }

        public int PlayerId { get; set; }

        public string SportId { get; set; }
    }
}
