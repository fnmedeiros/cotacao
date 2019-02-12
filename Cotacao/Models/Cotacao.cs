using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotacao.Controllers
{
    public class Cotacao
    {
        //[Key]
        public int Id { get; set; }
        //[Column("date")]
        public DateTime Date { get; set; }
        public string UsuarioCadastro { get; set; }
        public int MercadoId { get; set; }
        public virtual Mercado Mercado { get; set; }
        public List<Item> Itens { get; set; }
    }
}