using DocumentFormat.OpenXml.Wordprocessing;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using Serilog;
using Soren.Custom.Domain.Entities.Response;
using Soren.Custom.Infra.Helpers;
using Soren.Custom.Service.Interfaces;
using System.Diagnostics;

namespace Soren.Custom.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Worker> _logger;
        private readonly IHostApplicationLifetime _applicationLifetime;
        private readonly SftpClient _sftpClient;
        private readonly IServiceCustumer _serviceCustumer;


        public Worker(IConfiguration configuration, ILogger<Worker> logger, IHostApplicationLifetime hostApplicationLifetime, SftpClient sftpClient, IServiceCustumer serviceCustumer)
        {
            _configuration = configuration;
            _logger = logger;
            _applicationLifetime = hostApplicationLifetime;
            _sftpClient = sftpClient;
            _serviceCustumer = serviceCustumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            double timeForProcessInHours = _configuration.GetSection("SftpClient")
                                                         .GetValue<double>("TimeForProcess");

            TimeSpan interval = TimeSpan.FromHours(timeForProcessInHours);

            while (!stoppingToken.IsCancellationRequested)
            {
                var ACT = new Activity(Guid.NewGuid().ToString());
                ACT.Start();
                var ACTid = ACT.Id;

                var stopwatch = Stopwatch.StartNew();

                try
                {
                    Log.Information("Iniciando processamento de arquivos SFTP: ActivityId: {@activityId}", ACTid);

                    await _sftpClient.ConnectAsync(stoppingToken);

                    var brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                    DateTime brazilTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brazilTimeZone);
                    string formattedNow = brazilTime.ToString("yyyyMMdd");

                    string baseFileName = $"Data{formattedNow}.txt";
                    string? baseFilePathToReadFile = _configuration.GetSection("SftpClient").GetValue<string>("PathToReadFiles");
                    string filePathToReadFile = $"{baseFilePathToReadFile}{baseFileName}";

                    bool hasFileToReadToday = _sftpClient.Exists(filePathToReadFile);
                    if (!hasFileToReadToday)
                    {
                        Log.Warning("Arquivo não encontrado: ActivityId: {@activityId} | Arquivo: {@arquivo}", ACTid, filePathToReadFile);
                    }
                    else
                    {
                        Log.Information("Arquivo encontrado: ActivityId: {@activityId} | Arquivo: {@arquivo}", ACTid, filePathToReadFile);

                        using var file = _sftpClient.OpenRead(filePathToReadFile);

                        await _serviceCustumer.RemovePreviousBaseAsync();

                        var batches = CustomTxtHelper.ReadFileInBatchesAsync<Pagamento>(file, 5000, new TxtMapPagamento());

                        await foreach (var batch in batches)
                        {
                            await _serviceCustumer.ImImportDataAsync(batch);
                        }

                        string? basePathToMoveProcessedFiles = _configuration.GetSection("SftpClient")
                                                                             .GetValue<string>("PathToMoveProcessedFiles");

                        string filePathToMoveProcessedFiles = $"{basePathToMoveProcessedFiles}{baseFileName}";

                        await _sftpClient.RenameFileAsync(filePathToReadFile, filePathToMoveProcessedFiles, stoppingToken);
                    }

                    _sftpClient.Disconnect();
                }
                catch (Exception ex)
                {
                    Log.Error("Erro no processamento: ActivityId: {@activityId} | Exception: {@ex}", ACTid, ex);
                }
                finally
                {
                    stopwatch.Stop();

                    Log.Information("Finalizado: ActivityId: {@activityId} | Tempo (min): {@minutes}", ACTid, stopwatch.Elapsed.TotalMinutes);
                    Activity.Current?.Stop();
                }

                Log.Information("⏱ Aguardando {interval} até próxima execução...", interval);
                await Task.Delay(interval, stoppingToken);
            }

            _applicationLifetime.StopApplication();
        }

    }
}
