using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardapioWinPhone.Models
{
    class Produto
    { 
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public string Foto { get; set; }
        public string NomeDescricao { get; set; }
        public int Cardapio_Id { get; set; }

    }
}
