using LitobzorImport.Domain.Base;

namespace LitobzorImport.DataAccess.Contracts
{
    public interface IReadHierarchyRepository<T>
        where T: IDomainMarker
    {
        List<T> GetAllFull();
        int GetCount();
        IDomainConfig<T> Config { get; }
    }
}
