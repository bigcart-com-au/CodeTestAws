using CodeChallenge.Abstractions;
using CodeChallenge.Common;
using CodeChallenge.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("sport/{sportId}/depthcharts")]
    public partial class DepthChartController : Controller
    {
        private readonly IDepthChartService _depthChartService;

        public DepthChartController(IDepthChartService depthChartService)
        {
            _depthChartService = depthChartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepthCharts([FromRoute] string sportId, [FromQuery] QueryPlayersBehindThePlayerInDepthChart query)
        {
            if (IsQueryValid(query))
            {
                var playersBehindThePlayerResult = await _depthChartService.GetPlayersBehindThePlayerInDepthChart(sportId, query.Position, query.PlayerId.Value);
                if (playersBehindThePlayerResult.IsFailure)
                {
                    return new ObjectResult(new Error(playersBehindThePlayerResult.Error))
                    {
                        StatusCode = (int?)HttpStatusCode.BadRequest
                    };
                }

                return new OkObjectResult(playersBehindThePlayerResult.Value);
            }

            var result = await _depthChartService.GetDepthCharts(sportId);
            if (result.IsFailure) {
                return new ObjectResult(new Error(result.Error))
                {
                    StatusCode = (int?)HttpStatusCode.BadRequest
                };
            }

            return new OkObjectResult(result.Value);
        }

        private static bool IsQueryValid(QueryPlayersBehindThePlayerInDepthChart query)
        {
            return !string.IsNullOrWhiteSpace(query.Position) && (query.PlayerId != null && query.PlayerId > 0);
        }
    }
}
