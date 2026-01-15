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
    public class SaleService
    {
        private readonly IRepository<Sale> _saleRepository;
        private readonly IRepository<Product> _productRepository;

        public SaleService(IRepository<Sale> saleRepository, IRepository<Product> productRepository)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
        }

        public async Task CreateSaleAsync(CreateSaleDto dto, string tenantId)
        {
            var sale = new Sale
            {
                CustomerId = dto.CustomerId,
                TenantId = tenantId
            };

            foreach (var itemDto in dto.Items)
            {
                var product = await _productRepository.GetByIdAsync(itemDto.ProductId);
                if (product == null)
                    throw new Exception($"Produto {itemDto.ProductId} não encontrado.");

                // AQUI ESTÁ A MÁGICA DO DDD:
                // O serviço coordena, mas a entidade Produto valida se pode deduzir o estoque.
                product.DeductStock(itemDto.Quantity);

                // Adiciona item na venda
                sale.AddItem(product, itemDto.Quantity);

                // O repositório genérico precisa saber que o produto foi modificado
                await _productRepository.UpdateAsync(product);
            }

            await _saleRepository.AddAsync(sale);
        }
    }
}
