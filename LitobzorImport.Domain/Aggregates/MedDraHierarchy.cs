using LitobzorImport.Domain.Base;

namespace LitobzorImport.Domain.Aggregates
{
    public class MedDraHierarchy : IAggregateMarker
    {
        public long PtCode { get; set; }

        public long HltCode { get; set; }

        public long HlgtCode { get; set; }

        public long SocCode { get; set; }

        public long PtSocCode { get; set; }

        public string PtName { get; set; }

        public string HltName { get; set; }

        public string HlgtName { get;set; }

        public string SocName { get; set; }

        public string SocAbbrev { get; set; }

        public char PrimarySocFg { get; set; }
    }
}
