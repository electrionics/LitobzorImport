using LitobzorImport.Domain.Base;
using LitobzorImport.Domain.Relations;

namespace LitobzorImport.Domain.References
{
    public class PreferredTerm : IReferenceMarker
    {
        public long PtCode { get; set; }

        public string PtName { get; set; }

        public long PtSocCode { get; set; }

        public SystemOrganClass MainSystemOrganClass { get; set; }

        public List<HltPtRelation> HltPtRelations { get; set; }

        public List<LowLevelTerm> LowLevelTerms { get; set; }
    }
}
