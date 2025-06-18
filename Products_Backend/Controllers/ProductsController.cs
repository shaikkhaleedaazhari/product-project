using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductService.DTOs;
using ProductService.Models;
using ProductService.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductsController(ProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var list = await _repo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDTO>>(list));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(_mapper.Map<ProductDTO>(entity));
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct(CreateProductDTO dto)
        {
            var entity = _mapper.Map<Product>(dto);
            var created = await _repo.AddAsync(entity);
            return CreatedAtAction(nameof(GetProduct), new { id = created.ProductId },
                _mapper.Map<ProductDTO>(created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, CreateProductDTO dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            await _repo.UpdateAsync(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var toDelete = await _repo.GetByIdAsync(id);
            if (toDelete == null) return NotFound();

            await _repo.RemoveAsync(toDelete);
            return NoContent();
        }
    }
}