using Cotacao.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cotacao.Controllers
{
    public class meuContexto : DbContext
    {
        public meuContexto() : base("AppNameCString")
        {
        }

        public System.Data.Entity.DbSet<Mercado> mercados { get; set; }
        public System.Data.Entity.DbSet<Cotacao> cotacoes { get; set; }
        public System.Data.Entity.DbSet<Item> itens { get; set; }
    }
}