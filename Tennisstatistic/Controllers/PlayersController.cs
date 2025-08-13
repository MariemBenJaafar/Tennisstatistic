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

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAllPlayers());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var player = _service.GetPlayerById(id);
            if (player == null)
                return NotFound();
            return Ok(player);
        }

        [HttpPost]
        public IActionResult Create(CreatePlayerDto dto)
        {
            var newPlayer = _service.AddPlayer(dto);
            return CreatedAtAction(nameof(GetById), new { id = newPlayer.Id }, newPlayer);
        }
    }
}