namespace CodeChallenge.Configuration
{
    public interface IAppSettings
    {
        CosmosDbSettings CosmosDb { get; set; }
    }

    public class CosmosDbSettings
    {
        public string DatabaseName { get; set; }
        public string AccountEndpoint { get; set; }
    }
}
