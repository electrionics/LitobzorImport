
using LitobzorImport.Contracts;
using LitobzorImport.DataAccess.Contracts;
using LitobzorImport.Domain.Base;

namespace LitobzorImport.DataAccess
{
    public class ParseFileRepository<T>(IDomainConfig<T> domainConfig, IDataConfiguration configuration, IProgressNotifier progressNotifier) : IReadSourceRepository<T>
        where T : IDomainMarker
    {
        private readonly IDomainConfig<T> domainConfig = domainConfig;
        private readonly IDataConfiguration configuration = configuration;
        private readonly IProgressNotifier progressNotifier = progressNotifier;

        public IEnumerable<SourceReference<T>> GetAll()
        {
            try
            {
                var filePath = Path.Combine(configuration.FolderPath, domainConfig.FileName);

                using var reader = new StreamReader(filePath);

                var data = reader.ReadToEnd();

                var entityLines = data.Split('\n');
                for (var i = 0; i < entityLines.Length; i++)
                {
                    entityLines[i] = entityLines[i].Trim();
                }

                var toReturn = new List<SourceReference<T>>(entityLines.Length);

                var successLines = 0;
                for (var i = 0; i < entityLines.Length; i++)
                {
                    var line = entityLines[i];
                    var result = domainConfig.ParseLine(line);

                    if (result.Success)
                    {
                        successLines++;
                        toReturn.Add(new SourceReference<T> { Value = result.Value!, LineNumber = i + 1 });

                        // notify batch of lines successfully parsed
                        if (successLines != 0 && successLines % 100 == 0)
                        {
                            progressNotifier.NotifyProgress($"{domainConfig.DisplayName}: successfully parsed lines = {successLines}", $"batchSuccess");
                        }
                    }
                    else
                    {
                        // notify line parsing error
                        progressNotifier.NotifyProgress($"File {domainConfig.FileName}, Line {i + 1} error: {result.Error!}", $"lineError");
                    }
                }

                // notify remain batch successfully parsed
                if (successLines % 100 != 0)
                {
                    progressNotifier.NotifyProgress($"{domainConfig.DisplayName}: successfully parsed lines = {successLines}", $"batchSuccess");
                }

                toReturn.TrimExcess();

                // different notifications for various domain entity types
                if (typeof(T) is IReferenceMarker)
                {
                    progressNotifier.NotifyProgress($"Importing {domainConfig.DisplayName}... №{toReturn.Count} records imported", "fileSuccess");
                }
                else if (typeof(T) is IRelationMarker)
                {
                    progressNotifier.NotifyProgress($"- {domainConfig.DisplayName}: №{toReturn.Count} relations", "fileSuccess");
                }
                else if (typeof(T) is IAggregateMarker)
                {
                    progressNotifier.NotifyProgress($"Building hierarchy... №{toReturn.Count} records", "fileSuccess");
                }

                return toReturn;

            }
            catch (Exception ex)
            {
                progressNotifier.NotifyProgress($"{domainConfig.DisplayName} read file error", "readFileError");
                return [];
            }
        }
    }
}
