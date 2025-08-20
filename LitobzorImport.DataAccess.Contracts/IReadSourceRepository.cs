using LitobzorImport.Domain.Base;

namespace LitobzorImport.DataAccess.Contracts
{
    public interface IReadSourceRepository<T>
        where T : IDomainMarker
    {
        IEnumerable<SourceReference<T>> GetAll();
    }
}
