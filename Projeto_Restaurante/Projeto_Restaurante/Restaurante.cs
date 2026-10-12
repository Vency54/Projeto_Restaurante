using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Restaurante
{
    internal class Restaurante
    {
        private int proxPedido;
        private Pedido[] pedidos;
        private int quantidadePedidos;

        public int ProximoId => proxPedido;

        public Restaurante()
        {
            proxPedido = 1;
            pedidos = new Pedido[50];
            quantidadePedidos = 0;
        }

        public bool NovoPedido(Pedido pedido)
        {
            if (quantidadePedidos >= pedidos.Length)
                return false;

            if (pedido.Id != proxPedido)
                return false;

            pedidos[quantidadePedidos] = pedido;
            quantidadePedidos++;
            proxPedido++;

            return true;
        }

        public Pedido? BuscarPedido(int id)
        {
            for (int i = 0; i < quantidadePedidos; i++)
            {
                if (pedidos[i].Id == id)
                    return pedidos[i];
            }

            return null;
        }

        public Pedido? BuscarPedido(Pedido pedido)
        {
            return BuscarPedido(pedido.Id);
        }

        public bool CancelarPedido(Pedido pedido)
        {
            if (!pedido.Ativo)
                return false;

            pedido.Cancelar();
            return true;
        }

        public double CalcularTotalDia()
        {
            double total = 0;

            for (int i = 0; i < quantidadePedidos; i++)
            {
                if (pedidos[i].Ativo)
                {
                    total += pedidos[i].CalcularTotal();
                }
            }

            return total;
        }

        public void ListarPedidos()
        {
            bool encontrou = false;

            Console.WriteLine("\n===== PEDIDOS ATIVOS =====");

            for (int i = 0; i < quantidadePedidos; i++)
            {
                if (pedidos[i].Ativo)
                {
                    Console.WriteLine(
                        $"Pedido #{pedidos[i].Id} - " +
                        $"{pedidos[i].Cliente} - " +
                        $"{pedidos[i].CalcularTotal():C2}"
                    );

                    encontrou = true;
                }
            }

            if (!encontrou)
                Console.WriteLine("Nenhum pedido ativo.");

            Console.WriteLine(
                $"\nSoma geral do dia: {CalcularTotalDia():C2}"
            );
        }
    }
}
