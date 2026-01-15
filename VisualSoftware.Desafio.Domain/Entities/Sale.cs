using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftware.Desafio.Domain.Common;

namespace VisualSoftware.Desafio.Domain.Entities
{
    public class Sale : BaseEntity, ITenantEntity
    {
        public DateTime SaleDate { get; set; } = DateTime.UtcNow;
        public Guid CustomerId { get; set; }
        public decimal TotalAmount { get; private set; }
        public string TenantId { get; set; } = string.Empty;

        // Lista de itens da venda
        public List<SaleItem> Items { get; set; } = new();

        public void AddItem(Product product, int quantity)
        {
            var item = new SaleItem
            {
                ProductId = product.Id,
                Product = product,
                Quantity = quantity,
                UnitPrice = product.Price
            };

            Items.Add(item);
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            TotalAmount = Items.Sum(i => i.Quantity * i.UnitPrice);
        }
    }

    public class SaleItem : BaseEntity
    {
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
