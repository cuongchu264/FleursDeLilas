using FleursDeLilas.API.DTOs;
using FleursDeLilas.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FleursDeLilas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliesController : ControllerBase
    {
        private readonly ISupplyQueryService _queryService;
        private readonly ISupplyCommandService _commandService;

        public SuppliesController(ISupplyQueryService queryService, ISupplyCommandService commandService)
        {
            _queryService = queryService;
            _commandService = commandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _queryService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _queryService.GetByIdAsync(id);
            if (result == null) return NotFound(new { message = $"Supply with ID {id} not found." });
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSupplyDto dto)
        {
            var result = await _commandService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSupplyDto dto)
        {
            var success = await _commandService.UpdateAsync(id, dto);
            if (!success) return NotFound(new { message = $"Supply with ID {id} not found." });
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var success = await _commandService.DeleteByIdAsync(id);
            if (!success) return NotFound(new { message = $"Supply with ID {id} not found." });
            return NoContent();
        }
    }
}
