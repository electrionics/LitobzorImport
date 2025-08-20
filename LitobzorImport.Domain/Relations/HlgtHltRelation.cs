using LitobzorImport.Domain.Base;
using LitobzorImport.Domain.References;

namespace LitobzorImport.Domain.Relations
{
    public class HlgtHltRelation : IRelationMarker
    {
        public long HlgtCode { get; set; }

        public long HltCode { get; set; }

        public HighLevelGroupTerm HighLevelGroupTerm { get; set; }

        public HighLevelTerm HighLevelTerm { get; set; }
    }
}
