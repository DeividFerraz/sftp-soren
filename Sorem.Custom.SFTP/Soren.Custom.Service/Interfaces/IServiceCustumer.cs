using Soren.Custom.Domain.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soren.Custom.Service.Interfaces
{
    public interface IServiceCustumer
    {
        Task<ResponseCostumer> GetByAsyncCPF(string cpf);
    }
}
