using Soren.Custom.Domain.DTOs.Response;
using Soren.Custom.Domain.Entities.Response;
using Soren.Custom.Infra.Interfaces;
using Soren.Custom.Service.Interfaces;

namespace Soren.Custom.Service.Services
{
    public class ServiceCustumer : IServiceCustumer
    {
        private readonly IRepoInformacoesCliente _repoInformacoesCliente;

        public ServiceCustumer(IRepoInformacoesCliente repoInformacoesCliente)
        {
            _repoInformacoesCliente = repoInformacoesCliente;
        }

        public async Task<ResponseCostumer> GetByAsyncCPF(string cpf)
        {
            var infoCliente = await _repoInformacoesCliente.GetByAsyncCPF(cpf);

            var client = new ResponseCostumer
            {
                NomeUsuario = infoCliente.NomeUsuario,
                Email = infoCliente.Email,
                CpfCnpj = infoCliente.CpfCnpj,
                Rua = infoCliente.Rua,
                Numero = infoCliente.Numero,
                Bairro = infoCliente.Bairro,
                Cidade = infoCliente.Cidade,
                Estado = infoCliente.Estado,
                CEP = infoCliente.CEP,
                ValorDivida = infoCliente.ValorDivida,
                DataVencimento = infoCliente.DataVencimento,
                NomeCredor = infoCliente.NomeCredor,
                CNPJCredor = infoCliente.CNPJCredor
            };

            return client;
        }

        //Não é necessario, poderia chamar o TruncateTableAsync direto. 
        public async Task RemovePreviousBaseAsync()
        {
            await _repoInformacoesCliente.TruncateTableAsync();
        }
        //Não é necessario, poderia chamar o ImportDataAsync direto. 
        public async Task ImImportDataAsync(List<Pagamento> data)
        {
            await _repoInformacoesCliente.ImImportDataAsync(data);
        }
    }
}
