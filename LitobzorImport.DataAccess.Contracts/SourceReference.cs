using LitobzorImport.Domain.Base;

namespace LitobzorImport.DataAccess.Contracts
{
    public class SourceReference<T>
        where T: IDomainMarker
    {
        public required T Value { get; set; }
        public int LineNumber { get; set; }
    }
}
