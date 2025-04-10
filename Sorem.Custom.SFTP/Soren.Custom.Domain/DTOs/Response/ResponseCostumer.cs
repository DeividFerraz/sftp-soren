namespace Soren.Custom.Domain.DTOs.Response
{
    public class ResponseCostumer
    {
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string CpfCnpj { get; set; }

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
