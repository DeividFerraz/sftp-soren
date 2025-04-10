using Soren.Custom.Domain.DTOs.Response;
using Soren.Custom.Infra.Interfaces;
using Soren.Custom.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
    }
}
