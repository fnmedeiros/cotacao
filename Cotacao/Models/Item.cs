using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotacao.Controllers
{
    public class Item
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int QtdEmbalagem { get; set; }
        public decimal ValorTotal { get; set; }
        public int CotacaoId { get; set; }
        public virtual Cotacao Cotacao { get; set; }
    }
}