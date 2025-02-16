using Backend.Api.DTO.Game;
using Backend.Api.Services.Implementations;
using Backend.Api.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class GameController : ControllerBase
    {
        readonly IGameService _service;

        public GameController(IGameService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_service.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _service.GetById(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateGameDto dto)
        {
            try
            {
                string baseUrl = $"{Request.Scheme}://{Request.Host}";
              

           

                return Ok(await _service.CreateAsync(dto, baseUrl));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateGameDto dto)
        {
            try
            {
                await _service.Update(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            try
            {
                await _service.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("/api/[controller]/[action]")]
        public async Task<IActionResult> SoftDelete([FromForm] int id)
        {
            try
            {
                await _service.SoftDelete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
