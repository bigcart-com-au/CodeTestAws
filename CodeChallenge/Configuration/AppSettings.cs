namespace CodeChallenge.Configuration
{
    public class AppSettings : IAppSettings
    {
        public CosmosDbSettings CosmosDb { get; set; }
    }
}
