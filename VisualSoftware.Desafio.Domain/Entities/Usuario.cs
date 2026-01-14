using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftware.Desafio.Domain.Common;

namespace VisualSoftware.Desafio.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; } // Nunca armazene senhas em texto puro!
        public string Role { get; private set; } // Ex: "Admin", "Gerente", "Vendedor"
        public string TenantId { get; private set; } // Para Multi-tenancy

        protected Usuario() { }

        public Usuario(string nome, string email, string senhaHash, string role, string tenantId)
        {
            ValidateDomain(nome, email, senhaHash, role, tenantId);

            Nome = nome;
            Email = email;
            SenhaHash = senhaHash; // O hash deve ser gerado na camada de Application antes de chegar aqui
            Role = role;
            TenantId = tenantId;
        }


        public void AlterarSenha(string novaSenhaHash)
        {
            if (string.IsNullOrWhiteSpace(novaSenhaHash))
                throw new ArgumentException("A nova senha não pode ser vazia.");

            SenhaHash = novaSenhaHash;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AtualizarPerfil(string nome, string email, string role)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome inválido");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email inválido");

            Nome = nome;
            Email = email;

            if (!string.IsNullOrWhiteSpace(role))
            {
                Role = role;
            }

            UpdatedAt = DateTime.UtcNow;
        }

        private void ValidateDomain(string nome, string email, string senhaHash, string role, string tenantId)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório");
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email é obrigatório");
            if (string.IsNullOrWhiteSpace(senhaHash)) throw new ArgumentException("Senha é obrigatória");
            if (string.IsNullOrWhiteSpace(role)) throw new ArgumentException("Role é obrigatório");
            if (string.IsNullOrWhiteSpace(tenantId)) throw new ArgumentException("TenantId é obrigatório para garantir isolamento de dados");
        }
    }
}
