using LitobzorImport.Domain.Base;

namespace LitobzorImport.DataAccess.Contracts
{
    public class ParseResult<T>
        where T : IDomainMarker
    {
        public bool Success { get; set; }

        public string? Error { get; set; }

        public T? Value { get; set; }
    }
}
