using LitobzorImport.Domain.Base;
using LitobzorImport.Domain.Relations;

namespace LitobzorImport.Domain.References
{
    public class HighLevelTerm : IReferenceMarker
    {
        public long HltCode { get; set; }

        public string HltName { get; set; }

        public List<HlgtHltRelation> HlgtHltRelations { get; set; }

        public List<HltPtRelation> HltPtRelations { get; set; }
    }
}
