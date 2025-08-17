using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TennisStatistics.Api.DTOs;
using TennisStatistics.Api.Models;
using TennisStatistics.Api.Services;

namespace TennisStatistics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _service;

        public PlayersController(IPlayerService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a list of all players.
        /// </summary>
        /// <returns>A list of players.</returns>
        /// <response code="200">Returns the list of players</response>
        [HttpGet]
        public IActionResult GetAllPlayers()
        {
            return Ok(_service.GetAllPlayers());
        }

        /// <summary>
        /// Get a player by their ID.
        /// </summary>
        /// <param name="id">The unique ID of the player.</param>
        /// <returns>The player data if found; otherwise, a 404 Not Found.</returns>
        /// <response code="200">Returns the player</response>
        /// <response code="404">If the player is not found</response>
        [HttpGet("{id}")]
        public IActionResult GetPlayerById(int id)
        {
            var player = _service.GetPlayerById(id);
            if (player == null)
                return NotFound();
            return Ok(player);
        }

        /// <summary>
        /// Creates a new player.
        /// </summary>
        /// <param name="dto">The data needed to create a new player.</param>
        /// <returns>The newly created player with a 201 status and location header.</returns>
        /// <response code="201">Returns the newly created player</response>
        /// <response code="400">If the input data is invalid</response>
        [HttpPost]
        public IActionResult CreatePlayer(CreatePlayerDto dto)
        {
            var newPlayer = _service.AddPlayer(dto);
            return CreatedAtAction(nameof(GetPlayerById), new { id = newPlayer.Id }, newPlayer);
        }

        /// <summary>
        /// Update an existing player.
        /// </summary>
        /// <param name="id">Player's ID.</param>
        /// <param name="dto">Update player's data.</param>
        /// <returns>the player is updated ou 404 if not found.</returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PlayerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePlayer(int id, UpdatePlayerDto dto)
        {
            try
            {
                var updated = _service.UpdatePlayer(id, dto);
                if (updated == null)
                    return NotFound(new { message = "Player not found" });

                return Ok(updated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", detail = ex.Message });
            }
        }

        /// <summary>
        /// Delete an existing player.
        /// </summary>
        /// <param name="id">the Deleted Player's ID.</param>
        /// <returns>204 NoContent if deleted, 404 if not found.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] 
        [ProducesResponseType(StatusCodes.Status404NotFound)]  
        public IActionResult DeletePlayer(int id)
        {
            try
            {
                var deleted = _service.DeletePlayer(id);
                if (!deleted)
                    return NotFound(new { message = "Player not found" });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", detail = ex.Message });
            }
        }

    }
}