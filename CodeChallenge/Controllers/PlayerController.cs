using CodeChallenge.Abstractions;
using CodeChallenge.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("sport/{sportId}/player")]
    public class PlayerController
    {
        private readonly IDepthChartService _depthChartService;

        public PlayerController(IDepthChartService depthChartService)
        {
            _depthChartService = depthChartService;
        }

        [HttpPost]
        public async Task AddPlayer([FromRoute]int sportId, [FromBody]Player player) { 
            //Validate sportId
            //Validate player

            //call depthchart service to add player

            //return response created 201
        }
    }
}
