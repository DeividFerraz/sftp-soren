using Microsoft.Data.SqlClient;

namespace Soren.Custom.Infra.Helpers
{
    public static class SqlHelper
    {
        public static SqlConnection GetSqlConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
