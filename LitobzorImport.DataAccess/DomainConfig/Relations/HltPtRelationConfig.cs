using LitobzorImport.DataAccess.Contracts;
using LitobzorImport.Domain.Relations;
using System.Text.RegularExpressions;

namespace LitobzorImport.DataAccess.DomainConfig.Relations
{
    public class HltPtRelationConfig : IDomainConfig<HltPtRelation>
    {
        public string FileName => "hlt_pt.asc";

        public string DisplayName => "HLT-PT";

        public string LongDisplayName => DisplayName + " Relation";

        public ParseResult<HltPtRelation> ParseLine(string line)
        {
            var regex = new Regex("(\\d+)\\$(\\d+)\\$");
            var match = regex.Match(line);
            if (match != null && match.Groups.Count == 3)
            {
                if (long.TryParse(match.Groups[1].Value, out var hltCode) && long.TryParse(match.Groups[2].Value, out var ptCode))
                {
                    return new ParseResult<HltPtRelation>
                    {
                        Success = true,
                        Value = new HltPtRelation
                        {
                            HltCode = hltCode,
                            PtCode = ptCode
                        }
                    };
                }
                else
                {
                    return new ParseResult<HltPtRelation> { Success = false, Error = $"Error parsing file {FileName}: invalid number format" };
                }
            }
            else
            {
                return new ParseResult<HltPtRelation> { Success = false, Error = $"Error parsing file {FileName}: invalid line format" };
            }
        }
    }
}
