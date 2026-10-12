using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Restaurante
{
    internal class Pedido
    {
        private int id;
        private string cliente;
        private Item[] itens;
        private int quantidadeItens;
        private bool ativo;

        public int Id => id;
        public string Cliente => cliente;
        public bool Ativo => ativo;
        public int QuantidadeItens => quantidadeItens;

        public Pedido(int id, string cliente)
        {
            this.id = id;
            this.cliente = cliente;
            itens = new Item[10];
            quantidadeItens = 0;
            ativo = true;
        }

        public bool AdicionarItem(Item item)
        {
            if (!ativo || quantidadeItens >= itens.Length)
                return false;

            itens[quantidadeItens] = item;
            quantidadeItens++;

            return true;
        }

        public bool RemoverItem(Item item)
        {
            if (!ativo)
                return false;

            for (int i = 0; i < quantidadeItens; i++)
            {
                if (itens[i].Id == item.Id)
                {
                    for (int j = i; j < quantidadeItens - 1; j++)
                    {
                        itens[j] = itens[j + 1];
                    }

                    quantidadeItens--;
                    itens[quantidadeItens] = null!;

                    return true;
                }
            }

            return false;
        }

        public double CalcularTotal()
        {
            double total = 0;

            for (int i = 0; i < quantidadeItens; i++)
            {
                total += itens[i].Preco;
            }

            return total;
        }

        public string DadosDoPedido()
        {
            string dados = $"Pedido: {id}\n";
            dados += $"Cliente: {cliente}\n";
            dados += $"Status: {(ativo ? "Ativo" : "Cancelado")}\n";
            dados += "Itens:\n";

            if (quantidadeItens == 0)
            {
                dados += "Nenhum item cadastrado.\n";
            }
            else
            {
                for (int i = 0; i < quantidadeItens; i++)
                {
                    dados += $"  {itens[i]}\n";
                }
            }

            dados += $"Total: {CalcularTotal():C2}";

            return dados;
        }

        public void Cancelar()
        {
            ativo = false;
        }
    }
}
