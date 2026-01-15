using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VisualSoftware.Desafio.Application.DTOs;
using VisualSoftware.Desafio.Application.Interfaces;
using VisualSoftware.Desafio.Domain.Entities;
using VisualSoftware.Desafio.Application.Services;

namespace VisualAPI.Controllers
{

[ApiController]
    [Authorize] // Protege todas as rotas
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _service;
        private readonly VisualSoftware.Desafio.Domain.Interfaces.ITenantService _tenantService;

        public ProductsController(ProductService service, VisualSoftware.Desafio.Domain.Interfaces.ITenantService tenantService)
        {
            _service = service;
            _tenantService = tenantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            // O TenantID é pego automaticamente do token pelo ITenantService
            // mas o service precisa dele para salvar na entidade
            var tenantId = _tenantService.GetTenantId();
            var result = await _service.CreateAsync(dto, tenantId);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, JsonPatchDocument<Product> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            try
            {
                await _service.UpdatePartialAsync(id, patchDoc);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
