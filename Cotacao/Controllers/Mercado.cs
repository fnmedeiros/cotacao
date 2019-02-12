using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotacao.Controllers
{
    public class Mercado
    {
        //[Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Cotacao> Cotacoes { get; set; }
    }
}