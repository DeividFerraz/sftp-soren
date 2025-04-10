using Microsoft.Extensions.DependencyInjection;
using Soren.Custom.Infra.DbConnection;
using Soren.Custom.Infra.Interfaces;
using Soren.Custom.Infra.Repository;
using Soren.Custom.Service.Interfaces;
using Soren.Custom.Service.Services;

namespace Soren.Custom.Service.IDependencyInjection
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IServiceCustumer, ServiceCustumer>();
            services.AddSingleton<IRepoInformacoesCliente, RepoInformacoesCliente>();
            services.AddSingleton<ICustomDbConnectionFactory, CustomDbConnectionFactory>();
        }
    }
}
