using Soren.Custom.Domain.Entities.Response;

namespace Soren.Custom.Infra.Interfaces
{
    public interface IRepoInformacoesCliente
    {
        Task<Pagamento> GetByAsyncCPF(string cpf);

        Task TruncateTableAsync();

        Task ImImportDataAsync(List<Pagamento> data);
    }
}
