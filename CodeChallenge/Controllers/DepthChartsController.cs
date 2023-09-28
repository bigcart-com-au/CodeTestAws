using CodeChallenge.Abstractions;
using CodeChallenge.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("sport/{sportId}/depthcharts")]
    public class DepthChartsController : Controller
    {
        private readonly IDepthChartService _depthChartService;

        public DepthChartsController(IDepthChartService depthChartService)
        {
            _depthChartService = depthChartService;
        }

        [HttpGet]
        public IEnumerable<DepthChart> GetDepthCharts([FromQuery] QueryOnRemainingPlayersPosition query)
        {
            //Validate Query

            return _depthChartService.GetDepthCharts().Value;
        }
    }
}
