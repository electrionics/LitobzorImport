using LitobzorImport.Domain.Base;
using LitobzorImport.Domain.References;

namespace LitobzorImport.Domain.Relations
{
    public class HltPtRelation : IRelationMarker
    {
        public long HltCode { get; set; }

        public long PtCode { get; set; }

        public HighLevelTerm HighLevelTerm { get; set; }

        public PreferredTerm PreferredTerm { get; set; }
    }
}
