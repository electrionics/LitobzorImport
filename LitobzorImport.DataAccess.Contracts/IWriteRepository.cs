using LitobzorImport.Domain.Base;

namespace LitobzorImport.DataAccess.Contracts
{
    public interface IWriteRepository<T> : IWriteRepositoryBase
        where T : IDomainMarker
    {
        void Create(T entity);

        IDomainConfig<T> Config { get; }
    }
}
