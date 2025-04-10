using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.Text;
using Renci.SshNet.Sftp;

namespace Soren.Custom.Infra.Helpers
{
    public static class CustomTxtHelper
    {
        public static async IAsyncEnumerable<List<T>> ReadFileInBatchesAsync<T>(SftpFileStream fileStream, int batchSize = 100, ClassMap? map = null)
        {
            List<T> batch = new List<T>();

            var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true,
            };

            using (var reader = new StreamReader(fileStream, Encoding.UTF8))
            using (var csv = new CsvReader(reader, csvConfiguration))
            {
                if (map != null)
                    csv.Context.RegisterClassMap(map);

                while (await csv.ReadAsync())
                {
                    var record = csv.GetRecord<T>();
                    batch.Add(record);

                    if (batch.Count >= batchSize)
                    {
                        yield return batch;
                        batch = new List<T>();
                    }
                }
            }

            if (batch.Count > 0)
            {
                yield return batch;
            }
        }
    }
}
