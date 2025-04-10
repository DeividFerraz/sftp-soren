using Microsoft.Extensions.Configuration;
using Soren.Custom.Domain.Entities.Response;
using Soren.Custom.Infra.Interfaces;
using Dapper;


namespace Soren.Custom.Infra.Repository
{
    public class RepoInformacoesCliente : IRepoInformacoesCliente
    {
        private readonly IConfiguration _configuration;
        private readonly ICustomDbConnectionFactory _customDbConnectionFactory;

        private const string TABLE_NAME = "Pagamentos";

        public RepoInformacoesCliente(IConfiguration configuration, ICustomDbConnectionFactory customDbConnectionFactory)
        {
            _configuration = configuration;
            _customDbConnectionFactory = customDbConnectionFactory;
        }

        public async Task<Pagamento> GetByAsyncCPF(string cpf)
        {
            using var connectionDataBase = _customDbConnectionFactory.CreateConnection();
            await connectionDataBase.OpenAsync();

            var query = $@"SELECT TOP (1) * FROM {TABLE_NAME} WHERE CpfCnpj = '{cpf}'";

            return await connectionDataBase.QueryFirstOrDefaultAsync<Pagamento>(query);
        }
    }
}
