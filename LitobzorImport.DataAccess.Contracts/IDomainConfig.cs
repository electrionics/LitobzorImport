using LitobzorImport.Domain.Base;

namespace LitobzorImport.DataAccess.Contracts;

public interface IDomainConfig<T>
    where T: IDomainMarker
{
    string FileName { get; }

    string DisplayName {  get; }

    string LongDisplayName { get; }

    ParseResult<T> ParseLine(string line);
}
