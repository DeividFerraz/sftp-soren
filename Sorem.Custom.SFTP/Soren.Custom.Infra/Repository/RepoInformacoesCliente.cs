using Microsoft.Extensions.Configuration;
using Soren.Custom.Domain.Entities.Response;
using Soren.Custom.Infra.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;


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

        public async Task TruncateTableAsync()
        {
            using var connectionDataBase = _customDbConnectionFactory.CreateConnection();
            await connectionDataBase.OpenAsync();

            var query = $@"TRUNCATE table {TABLE_NAME}";

            await connectionDataBase.ExecuteScalarAsync(query);
        }

        public async Task ImImportDataAsync(List<Pagamento> data)
        {
            using var connectionDataBase = _customDbConnectionFactory.CreateConnection();
            await connectionDataBase.OpenAsync();

            //Garante que tudo seja aplicado ou nada, se algo der erro, nada sera aplicado.
            using var transaction = connectionDataBase.BeginTransaction();

            try
            {
                using var SqlBulk = new SqlBulkCopy(connectionDataBase, SqlBulkCopyOptions.Default, transaction)
                {
                    DestinationTableName = TABLE_NAME
                };

                var table = CreateDataTableFromList(data);

                //Envia a dataBase criada para o banco de dados
                await SqlBulk.WriteToServerAsync(table);

                //Efetiva as alterações
                transaction.Commit();
            } catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        private static DataTable CreateDataTableFromList<T>(List<T> data)
        {
            DataTable table = new DataTable();
            var properties = typeof(T).GetProperties();

            foreach (var prop in properties)
            {
                Type propType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                table.Columns.Add(prop.Name, propType);
            }

            foreach (var item in data)
            {
                var values = properties.Select(prop => prop.GetValue(item)).ToArray();
                table.Rows.Add(values);
            }

            return table;
        }
    }
}
