using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftware.Desafio.Application.DTOs;
using VisualSoftware.Desafio.Domain.Entities;
using VisualSoftware.Desafio.Domain.Interfaces;

namespace VisualSoftware.Desafio.Application.Interfaces
{
    public class ProductService
    {
        private readonly IRepository<Produto> _repository;

        public ProductService(IRepository<Produto> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            // Mapeamento manual (Em projetos reais usamos AutoMapper ou Mapster)
            return products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.StockQuantity));
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto, string tenantId)
        {
            var product = new Produto
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                TenantId = tenantId // Importante: Setamos o Tenant aqui!
            };

            await _repository.AddAsync(product);
            return new ProductDto(product.Id, product.Name, product.Price, product.StockQuantity);
        }

        // Método PATCH (Atualização Parcial)
        public async Task UpdatePartialAsync(int id, JsonPatchDocument<Produto> patchDoc)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) throw new KeyNotFoundException("Produto não encontrado");

            // Aplica as mudanças do JSON Patch na entidade
            patchDoc.ApplyTo(product);

            await _repository.UpdateAsync(product);
        }
    }
}
