using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftware.Desafio.Domain.Entities;

namespace VisualSoftware.Desafio.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        // O TenantID virá de um serviço que lê o Header da requisição
        private readonly string _tenantId;

        public AppDbContext(DbContextOptions<AppDbContext> options, ITenantService tenantService) : base(options)
        {
            _tenantId = tenantService.GetTenantId();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // CONFIGURAÇÃO GLOBAL DE MULTI-TENANCY
            // Isso aplica um filtro "WHERE TenantId = '...'" em TODAS as consultas automaticamente.
            builder.Entity<Product>().HasQueryFilter(a => a.TenantId == _tenantId);
            builder.Entity<Sale>().HasQueryFilter(a => a.TenantId == _tenantId);
            builder.Entity<User>().HasQueryFilter(a => a.TenantId == _tenantId);

            // Configuração de precisão para dinheiro (obrigatório para Postgres/C# decimal)
            builder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
            builder.Entity<SaleItem>().Property(i => i.UnitPrice).HasPrecision(18, 2);
        }
    }
}
