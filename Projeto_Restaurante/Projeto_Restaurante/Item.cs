using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Restaurante
{
    public class Item
    {
        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public double Preco { get; private set; }

        public Item(int id, string descricao, double preco)
        {
            Id = id;
            Descricao = descricao;
            Preco = preco;
        }

        public override string ToString()
        {
            return $"ID: {Id} | {Descricao} | {Preco:C2}";
        }
    }
}
