using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftware.Desafio.Application.DTOs;
using VisualSoftware.Desafio.Domain.Entities;
using VisualSoftware.Desafio.Domain.Interfaces;

namespace VisualSoftware.Desafio.Application.Services
{
    public class ProductService
    {
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.StockQuantity));
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto, string tenantId)
        {
            // Aqui usamos o construtor da entidade que valida as regras (preço negativo, etc)
            var product = new Product(dto.Name, dto.Description, dto.Price, dto.StockQuantity, tenantId);

            await _repository.AddAsync(product);

            return new ProductDto(product.Id, product.Name, product.Price, product.StockQuantity);
        }

        // Método PATCH usando GUID
        public async Task UpdatePartialAsync(Guid id, JsonPatchDocument<Product> patchDoc)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) throw new KeyNotFoundException("Produto não encontrado");

            // Aplica as mudanças do JSON Patch na entidade
            patchDoc.ApplyTo(product);

            await _repository.UpdateAsync(product);
        }
    }
}
