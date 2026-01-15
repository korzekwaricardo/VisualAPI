using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftware.Desafio.Domain.Common;

namespace VisualSoftware.Desafio.Domain.Entities
{
    public class Product : BaseEntity, ITenantEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        public string TenantId { get; set; } = string.Empty;

        // Construtor vazio para o EF Core
        protected Product() { }

        public Product(string name, string description, decimal price, int stockQuantity, string tenantId)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nome é obrigatório");
            if (price < 0) throw new ArgumentException("Preço não pode ser negativo");

            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            TenantId = tenantId;
        }

        public void DeductStock(int quantity)
        {
            if (quantity <= 0) throw new ArgumentException("Quantidade deve ser maior que zero.");
            if (StockQuantity < quantity) throw new InvalidOperationException("Estoque insuficiente.");

            StockQuantity -= quantity;
        }

        public void UpdateData(string name, decimal price)
        {
            if (!string.IsNullOrEmpty(name)) Name = name;
            if (price >= 0) Price = price;
        }
    }
}
