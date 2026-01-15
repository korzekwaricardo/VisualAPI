using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftware.Desafio.Application.DTOs
{
    public record CreateSaleItemDto(int ProductId, int Quantity);
    public record CreateSaleDto(int CustomerId, List<CreateSaleItemDto> Items);
}
