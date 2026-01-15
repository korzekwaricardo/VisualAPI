using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualSoftware.Desafio.Application.DTOs
{
    public record CreateSaleItemDto(Guid ProductId, int Quantity);
    public record CreateSaleDto(Guid CustomerId, List<CreateSaleItemDto> Items);
}
