using CsvHelper.Configuration;
using Soren.Custom.Domain.Entities.Response;
using System.Globalization;

namespace Soren.Custom.Infra.Helpers
{
    public class TxtMapPagamento : ClassMap<Pagamento>
    {
        public TxtMapPagamento()
        {
            AutoMap(CultureInfo.InstalledUICulture);

            Map(x => x.Id).Ignore();

            Map(x => x.NomeUsuario).Name("NomeUsuario");
            Map(x => x.Email).Name("Email");
            Map(x => x.Rua).Name("Rua");
            Map(x => x.Numero).Name("Numero");
            Map(x => x.Bairro).Name("Bairro");
            Map(x => x.Cidade).Name("Cidade");
            Map(x => x.Estado).Name("Estado");
            Map(x => x.CEP).Name("CEP");
            Map(x => x.ValorDivida).Name("ValorDivida");
            Map(x => x.DataVencimento).Name("DataVencimento");
            Map(x => x.NomeCredor).Name("NomeCredor");
            Map(x => x.CNPJCredor).Name("CNPJCredor");
            Map(x => x.CpfCnpj).Name("CpfCnpj");
        }
    }
}
