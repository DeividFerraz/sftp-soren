using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soren.Custom.Domain.Entities.Response
{
    public class Pagamento
    {
        public int Id { get; set; }

        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string CpfCnpj {  get; set; }

        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }

        public decimal ValorDivida { get; set; }
        public DateTime DataVencimento { get; set; }

        public string NomeCredor { get; set; }
        public string CNPJCredor { get; set; }
    }
}
