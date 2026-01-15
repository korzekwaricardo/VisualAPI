using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftware.Desafio.Domain.Common;

namespace VisualSoftware.Desafio.Domain.Entities
{
    public class User : BaseEntity, ITenantEntity
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public string TenantId { get; set; } = string.Empty;
    }
}
