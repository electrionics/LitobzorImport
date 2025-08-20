using LitobzorImport.Domain.Base;
using LitobzorImport.Domain.References;

namespace LitobzorImport.Domain.Relations
{
    public class SocHlgtRelation : IRelationMarker
    {
        public long SocCode { get; set; }

        public long HlgtCode { get; set; } 

        public SystemOrganClass SystemOrganClass { get; set; }

        public HighLevelGroupTerm HighLevelGroupTerm { get; set; }
    }
}
