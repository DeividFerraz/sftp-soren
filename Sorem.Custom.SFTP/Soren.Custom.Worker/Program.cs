using Soren.Custom.Service.IDependencyInjection;
using Soren.Custom.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddSftpConnections(builder.Configuration);
builder.Services.AddDependencies();

builder.Logging.ClearProviders();

var host = builder.Build();
host.Run();
