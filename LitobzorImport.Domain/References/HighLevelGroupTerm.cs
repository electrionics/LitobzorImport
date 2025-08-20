using LitobzorImport.Domain.Base;
using LitobzorImport.Domain.Relations;

namespace LitobzorImport.Domain.References
{
    public class HighLevelGroupTerm : IReferenceMarker
    {
        public long HlgtCode { get; set; }

        public string HlgtName { get; set; }

        public List<HlgtHltRelation> HlgtHltRelations { get; set; }

        public List<SocHlgtRelation> SocHlgtRelations { get; set; }
    }
}
