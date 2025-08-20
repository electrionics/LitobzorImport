using LitobzorImport.Domain.Base;

namespace LitobzorImport.Domain.References
{
    /// <summary>
    /// LLT
    /// </summary>
    public class LowLevelTerm : IReferenceMarker
    {
        public long LltCode { get; set; }

        public string LltName { get; set; }

        public long PtCode { get; set; }

        public char LltCurrency { get; set; }

        public PreferredTerm PreferredTerm { get; set; }
    }
}
