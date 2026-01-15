using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftware.Desafio.Application.DTOs
{
    public record ProductDto(int Id, string Name, decimal Price, int StockQuantity);
    public record CreateProductDto(string Name, string Description, decimal Price, int StockQuantity);
}
