using LitobzorImport.DataAccess.Contracts;
using LitobzorImport.Domain;
using LitobzorImport.Domain.Base;

namespace LitobzorImport.DataAccess
{
    public class ReadDbRepository<T>(LitobzorDataContext dataContext, IDomainConfig<T> config) : IReadHierarchyRepository<T>
        where T : class, IDomainMarker
    {
        protected readonly LitobzorDataContext dataContext = dataContext;
        private readonly IDomainConfig<T> config = config;

        public IDomainConfig<T> Config => config;

        public virtual List<T> GetAllFull()
        {
            throw new NotSupportedException();
        }

        public int GetCount()
        {
            return dataContext.Set<T>().Count();
        }
    }
}
