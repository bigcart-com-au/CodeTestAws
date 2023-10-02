using FluentAssertions;

namespace CodeChallenge.Integration.Tests
{
    public class GetDepthChartTest
    {
        IntegrationTestClient _testClient;
        public GetDepthChartTest()
        {
            _testClient = new IntegrationTestClient();
        }

        //[Fact] TODO: Fix test setup
        public async Task GetDepthChart_WhenValidSportIdProvided_ThenGetsAllDepthchart() {
            // Arrange
            var sportId = "1";

            // Act
            var response = await _testClient.GetDepthChart(sportId);

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}
