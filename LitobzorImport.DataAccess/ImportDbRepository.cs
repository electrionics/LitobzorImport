using LitobzorImport.DataAccess.Contracts;
using LitobzorImport.Domain;
using LitobzorImport.Domain.Base;

namespace LitobzorImport.DataAccess
{
    public class ImportDbRepository<T> : IWriteRepository<T>
        where T : class, IDomainMarker
    {
        private readonly LitobzorDataContext dataContext;
        private readonly IDomainConfig<T> domainConfig;

        public ImportDbRepository(LitobzorDataContext dataContext, IDomainConfig<T> domainConfig)
        {
            this.dataContext = dataContext;
            this.domainConfig = domainConfig;
        }

        public IDomainConfig<T> Config => domainConfig;

        public void CleanDatabase()
        {
            dataContext.Database.EnsureDeleted();
            dataContext.Database.EnsureCreated();
        }

        public void Create(T entity)
        {
            dataContext.Set<T>().Add(entity);
            dataContext.SaveChanges();
        }
    }
}
