using Microsoft.Data.SqlClient;

namespace Soren.Custom.Infra.Interfaces
{
    public  interface ICustomDbConnectionFactory
    {
       SqlConnection CreateConnection();
    }
}
