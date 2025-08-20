using LitobzorImport.DataAccess.Contracts;
using LitobzorImport.Domain.Relations;
using System.Text.RegularExpressions;

namespace LitobzorImport.DataAccess.DomainConfig.Relations
{
    public class HlgtHltRelationConfig : IDomainConfig<HlgtHltRelation>
    {
        public string FileName => "hlgt_hlt.asc";

        public string DisplayName => "HLGT-HLT";

        public string LongDisplayName => DisplayName + " Relation";

        public ParseResult<HlgtHltRelation> ParseLine(string line)
        {
            var regex = new Regex("(\\d+)\\$(\\d+)\\$");
            var match = regex.Match(line);
            if (match != null && match.Groups.Count == 3)
            {
                if (long.TryParse(match.Groups[1].Value, out var hlgtCode) && long.TryParse(match.Groups[2].Value, out var hltCode))
                {
                    return new ParseResult<HlgtHltRelation>
                    {
                        Success = true,
                        Value = new HlgtHltRelation
                        {
                            HlgtCode = hlgtCode,
                            HltCode = hltCode
                        }
                    };
                }
                else
                {
                    return new ParseResult<HlgtHltRelation> { Success = false, Error = $"Error parsing file {FileName}: invalid number format" };
                }
            }
            else
            {
                return new ParseResult<HlgtHltRelation> { Success = false, Error = $"Error parsing file {FileName}: invalid line format" };
            }
        }
    }
}
