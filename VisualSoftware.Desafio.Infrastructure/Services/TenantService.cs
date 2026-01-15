using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftware.Desafio.Domain.Interfaces;

namespace VisualSoftware.Desafio.Infrastructure.Services
{
    public class TenantService : ITenantService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetTenantId()
        {
            // Tenta pegar do Token JWT (Claim)
            var tenantId = _httpContextAccessor.HttpContext?.User?.FindFirst("tenant_id")?.Value;

            // Se não estiver logado, tenta pegar do Header (útil para o momento do Login)
            if (string.IsNullOrEmpty(tenantId))
            {
                tenantId = _httpContextAccessor.HttpContext?.Request.Headers.FirstOrDefault();
            }

            return tenantId ?? string.Empty;
        }
    }
}
