using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cotacao.ViewModel
{
    public class CotacaoViewModel
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string UsuarioCadastro { get; set; }
        public int MercadoId { get; set; }
    }
}