using CodeChallenge.Integration.Tests.Fakes;
using CodeChallenge.Repositories;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace CodeChallenge.Integration.Tests
{
    public class IntegrationTestClient
    {
        private readonly HttpClient _httpClient;

        public IntegrationTestClient()
        {
            IWebHostBuilder builder = WebHost
                .CreateDefaultBuilder()
                .UseContentRoot(AppContext.BaseDirectory)
                .UseStartup(typeof(Startup))
                .ConfigureTestServices(services => {
                    services.AddScoped<IDepthChartRepository, FakeDepthChartRepository>();
                    services.AddScoped<ISportRepository, FakeSportRepository>();
                 });

            var server = new TestServer(builder);
            _httpClient = server.CreateClient();
        }

        public async Task<HttpResponseMessage> GetDepthChart(string sportId)
        {
            return await _httpClient.GetAsync($"/sport/{sportId}/depthchart");
        }
    }
}
