
using Projeto_Restaurante;
using System.Globalization;

namespace Projeto_Restaurante
{
    internal class Program
    {
        private static readonly Restaurante restaurante = new();
        private static int proximoIdItem = 1;

        private static readonly CultureInfo cultura =
            CultureInfo.GetCultureInfo("pt-BR");

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int opcao;

            do
            {
                ExibirMenu();
                opcao = LerInteiro("Escolha uma opção: ");

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Encerrando o sistema...");
                        break;
                    case 1:
                        CriarPedido();
                        break;
                    case 2:
                        AdicionarItem();
                        break;
                    case 3:
                        RemoverItem();
                        break;
                    case 4:
                        ConsultarPedido();
                        break;
                    case 5:
                        CancelarPedido();
                        break;
                    case 6:
                        restaurante.ListarPedidos();
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine(
                        "\nPressione ENTER para continuar...");
                    Console.ReadLine();
                }

            } while (opcao != 0);
        }

        static void ExibirMenu()
        {
            Console.Clear();
            Console.WriteLine("===== COZINHA INDUSTRIAL =====");
            Console.WriteLine("0 - Sair");
            Console.WriteLine("1 - Criar novo pedido");
            Console.WriteLine("2 - Adicionar item ao pedido");
            Console.WriteLine("3 - Remover item do pedido");
            Console.WriteLine("4 - Consultar pedido");
            Console.WriteLine("5 - Cancelar pedido");
            Console.WriteLine("6 - Listar todos os pedidos");
            Console.WriteLine("==============================");
        }

        static void CriarPedido()
        {
            if (restaurante.ProximoId > 50)
            {
                Console.WriteLine(
                    "Limite diário de pedidos atingido.");
                return;
            }

            Console.Write("Nome do cliente: ");
            string cliente = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(cliente))
            {
                Console.WriteLine(
                    "O nome do cliente é obrigatório.");
                return;
            }

            Pedido pedido = new(restaurante.ProximoId, cliente);

            if (restaurante.NovoPedido(pedido))
                Console.WriteLine(
                    $"Pedido #{pedido.Id} criado com sucesso!");
            else
                Console.WriteLine(
                    "Não foi possível criar o pedido.");
        }

        static void AdicionarItem()
        {
            int idPedido = LerInteiro("ID do pedido: ");
            Pedido? pedido = restaurante.BuscarPedido(idPedido);

            if (pedido == null)
            {
                Console.WriteLine("Pedido não encontrado.");
                return;
            }

            if (!pedido.Ativo)
            {
                Console.WriteLine(
                    "Não é possível alterar um pedido cancelado.");
                return;
            }

            if (pedido.QuantidadeItens >= 10)
            {
                Console.WriteLine(
                    "O pedido já possui 10 itens.");
                return;
            }

            Console.Write("Descrição do item: ");
            string descricao = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(descricao))
            {
                Console.WriteLine("A descrição é obrigatória.");
                return;
            }

            double preco = LerPreco("Preço do item: R$ ");

            if (preco < 0)
            {
                Console.WriteLine(
                    "O preço não pode ser negativo.");
                return;
            }

            Item item = new(proximoIdItem, descricao, preco);

            if (pedido.AdicionarItem(item))
            {
                proximoIdItem++;
                Console.WriteLine(
                    $"Item #{item.Id} adicionado com sucesso!");
            }
            else
            {
                Console.WriteLine(
                    "Não foi possível adicionar o item.");
            }
        }

        static void RemoverItem()
        {
            int idPedido = LerInteiro("ID do pedido: ");
            Pedido? pedido = restaurante.BuscarPedido(idPedido);

            if (pedido == null)
            {
                Console.WriteLine("Pedido não encontrado.");
                return;
            }

            if (!pedido.Ativo)
            {
                Console.WriteLine("O pedido está cancelado.");
                return;
            }

            Console.WriteLine(pedido.DadosDoPedido());

            int idItem = LerInteiro(
                "\nID do item que deseja remover: ");

            Item item = new(idItem, "", 0);

            if (pedido.RemoverItem(item))
                Console.WriteLine("Item removido com sucesso!");
            else
                Console.WriteLine(
                    "Item não encontrado nesse pedido.");
        }

        static void ConsultarPedido()
        {
            int id = LerInteiro("ID do pedido: ");
            Pedido? pedido = restaurante.BuscarPedido(id);

            if (pedido == null)
            {
                Console.WriteLine("Pedido não encontrado.");
                return;
            }

            Console.WriteLine("\n===== DADOS DO PEDIDO =====");
            Console.WriteLine(pedido.DadosDoPedido());
        }

        static void CancelarPedido()
        {
            int id = LerInteiro("ID do pedido a cancelar: ");
            Pedido? pedido = restaurante.BuscarPedido(id);

            if (pedido == null)
            {
                Console.WriteLine("Pedido não encontrado.");
                return;
            }

            if (restaurante.CancelarPedido(pedido))
                Console.WriteLine(
                    $"Pedido #{id} cancelado com sucesso!");
            else
                Console.WriteLine(
                    "Esse pedido já está cancelado.");
        }

        static int LerInteiro(string mensagem)
        {
            int valor;

            while (true)
            {
                Console.Write(mensagem);

                if (int.TryParse(Console.ReadLine(), out valor))
                    return valor;

                Console.WriteLine(
                    "Digite um número inteiro válido.");
            }
        }

        static double LerPreco(string mensagem)
        {
            double valor;

            while (true)
            {
                Console.Write(mensagem);
                string entrada = Console.ReadLine() ?? "";

                if (double.TryParse(
                    entrada,
                    NumberStyles.Currency,
                    cultura,
                    out valor))
                    return valor;

                if (double.TryParse(
                    entrada,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out valor))
                    return valor;

                Console.WriteLine(
                    "Digite um preço válido, por exemplo 12,50.");
            }
        }
    }
}
