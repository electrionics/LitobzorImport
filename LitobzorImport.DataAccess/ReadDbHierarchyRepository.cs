using LitobzorImport.DataAccess.Contracts;
using LitobzorImport.Domain;
using LitobzorImport.Domain.References;
using Microsoft.EntityFrameworkCore;

namespace LitobzorImport.DataAccess
{
    public class ReadDbHierarchyRepository(LitobzorDataContext dataContext, IDomainConfig<SystemOrganClass> config)
        : ReadDbRepository<SystemOrganClass>(dataContext, config)
    {
        public override List<SystemOrganClass> GetAllFull()
        {
            return dataContext.Set<SystemOrganClass>().AsNoTracking()
                .Include(x => x.SocHlgtRelations)
                .ThenInclude(x => x.HighLevelGroupTerm)
                .ThenInclude(x => x.HlgtHltRelations)
                .ThenInclude(x => x.HighLevelTerm)
                .ThenInclude(x => x.HltPtRelations)
                .ThenInclude(x => x.PreferredTerm)
                .ThenInclude(x => x.LowLevelTerms)
                .Include(x => x.PreferredTerms)
                .ToList();
        }
    }
}
