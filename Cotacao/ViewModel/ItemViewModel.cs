using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotacao.ViewModel
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int QtdEmbalagem { get; set; }
        public decimal ValorTotal { get; set; }
        public int CotacaoId { get; set; }
    }
}