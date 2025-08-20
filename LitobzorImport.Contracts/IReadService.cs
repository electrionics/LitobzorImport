using LitobzorImport.DataAccess.Contracts;
using LitobzorImport.Domain.Aggregates;
using LitobzorImport.Domain.References;
using LitobzorImport.Domain.Relations;

namespace LitobzorImport.Contracts
{
    public interface IReadService
    {
        List<SourceReference<HighLevelGroupTerm>> ReadHighLevelGroupTerms();
        List<SourceReference<HighLevelTerm>> ReadHighLevelTerms();
        List<SourceReference<LowLevelTerm>> ReadLowLevelTerms();
        List<SourceReference<PreferredTerm>> ReadPreferredTerms();
        List<SourceReference<SystemOrganClass>> ReadSystemOrganClasses();

        List<SourceReference<HlgtHltRelation>> ReadHlgtHltRelations();
        List<SourceReference<HltPtRelation>> ReadHltPtRelations();
        List<SourceReference<SocHlgtRelation>> ReadHlgtSocRelations();

        List<SystemOrganClass> ReadHierarchyFull();

        List<SourceReference<MedDraHierarchy>> ReadMedDraHierarchies();

        List<(string, int)> ReadStatistics();
    }
}
