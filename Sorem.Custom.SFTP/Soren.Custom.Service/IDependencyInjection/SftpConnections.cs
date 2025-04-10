using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Renci.SshNet;


namespace Soren.Custom.Service.IDependencyInjection
{
    public static class SftpConnections
    {
        public static void AddSftpConnections(this IServiceCollection services, IConfiguration configuration)
        {
            IConfigurationSection sectionClientSftp = configuration.GetSection("SftpClient");

            var connectionInfo = new ConnectionInfo(host: sectionClientSftp.GetValue<string>("Host"),
                                                    port: sectionClientSftp.GetValue<int>("Port"),
                                                    username: sectionClientSftp.GetValue<string>("User"),
                                                    new PasswordAuthenticationMethod(sectionClientSftp.GetValue<string>("User"),
                                                                                     sectionClientSftp.GetValue<string>("Password"))
            )
            {
                Timeout = TimeSpan.FromSeconds(sectionClientSftp.GetValue<double>("TimeoutInSeconds"))
            };

            services.AddSingleton(provider => new SftpClient(connectionInfo));
        }
    }
}
