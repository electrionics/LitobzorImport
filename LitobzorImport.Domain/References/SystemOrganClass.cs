using LitobzorImport.Domain.Base;
using LitobzorImport.Domain.Relations;

namespace LitobzorImport.Domain.References
{
    public class SystemOrganClass : IReferenceMarker
    {
        public long SocCode { get; set; }

        public string SocName { get; set; }

        public string SocAbbrev { get; set; }

        public List<SocHlgtRelation> SocHlgtRelations { get; set; }

        public List<PreferredTerm> PreferredTerms { get; set; }
    }
}
