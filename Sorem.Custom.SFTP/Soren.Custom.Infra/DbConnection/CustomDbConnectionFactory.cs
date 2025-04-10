using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Soren.Custom.Infra.Helpers;
using Soren.Custom.Infra.Interfaces;

namespace Soren.Custom.Infra.DbConnection
{
    public class CustomDbConnectionFactory : ICustomDbConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private const string CONNECTION_STRING_KEY = "SorenProjects";

        public CustomDbConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection CreateConnection()
        {
            var connectionString = _configuration.GetConnectionString(CONNECTION_STRING_KEY);
            return SqlHelper.GetSqlConnection(connectionString);
        }
    }

}
