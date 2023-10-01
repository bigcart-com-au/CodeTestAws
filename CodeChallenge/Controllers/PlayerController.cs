using CodeChallenge.Abstractions;
using CodeChallenge.Common;
using CodeChallenge.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("sport/{sportId}/player")]
    public class PlayerController
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer([FromRoute] string sportId, [FromBody] Player player)
        {
            //Validate sportId
            //Validate player

            var result = await _playerService.AddPlayer(sportId, player);

            if (result.IsFailure)
            {
                new ObjectResult(new Error(result.Error))
                {
                    StatusCode = (int?)HttpStatusCode.InternalServerError
                };
            }

            return new CreatedResult("", player);
        }


        [HttpDelete]
        [Route("{playerId}")]
        public async Task<IActionResult> RemovePlayer([FromRoute] string sportId, [FromRoute] int playerId)
        {
            //Validate sportId
            //Validate player

            var result = await _playerService.RemovePlayer(sportId, playerId);

            if (!result.Success)
            {
                new ObjectResult(new Error(result.Error))
                {
                    StatusCode = (int?)HttpStatusCode.InternalServerError
                };
            }

            return new OkResult();
        }
    }
}
