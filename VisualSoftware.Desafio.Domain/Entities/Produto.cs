using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualSoftware.Desafio.Domain.Common;

namespace VisualSoftware.Desafio.Domain.Entities
{
    public class Produto : BaseEntity
    {
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
        public int Estoque { get; private set; }

        public Produto(string nome, decimal preco, int estoque)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome é obrigatório");
            if (preco < 0)
                throw new ArgumentException("Preço não pode ser negativo");

            Nome = nome;
            Preco = preco;
            Estoque = estoque;
        }

        public void AtualizarEstoque(int quantidade)
        {
            if (quantidade < 0) throw new ArgumentException("Estoque inválido");
            Estoque = quantidade;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AtualizarDados(string nome, decimal preco)
        {
            Nome = nome;
            Preco = preco;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
