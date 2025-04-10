using Soren.Custom.Domain.DTOs.Response;
using Soren.Custom.Domain.Entities.Response;

namespace Soren.Custom.Service.Interfaces
{
    public interface IServiceCustumer
    {
        Task<ResponseCostumer> GetByAsyncCPF(string cpf);
        Task RemovePreviousBaseAsync();
        Task ImImportDataAsync(List<Pagamento> data);
    }
}
