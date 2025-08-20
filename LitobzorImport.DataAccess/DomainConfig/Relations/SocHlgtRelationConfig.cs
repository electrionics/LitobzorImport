using LitobzorImport.DataAccess.Contracts;
using LitobzorImport.Domain.Relations;
using System.Text.RegularExpressions;

namespace LitobzorImport.DataAccess.DomainConfig.Relations
{
    public class SocHlgtRelationConfig : IDomainConfig<SocHlgtRelation>
    {
        public string FileName => "soc_hlgt.asc";

        public string DisplayName => "SOC-HLGT";

        public string LongDisplayName => DisplayName + " Relation";

        public ParseResult<SocHlgtRelation> ParseLine(string line)
        {
            var regex = new Regex("(\\d+)\\$(\\d+)\\$");
            var match = regex.Match(line);
            if (match != null && match.Groups.Count == 3)
            {
                if (long.TryParse(match.Groups[1].Value, out var socCode) && long.TryParse(match.Groups[2].Value, out var hlgtCode))
                {
                    return new ParseResult<SocHlgtRelation>
                    {
                        Success = true,
                        Value = new SocHlgtRelation
                        {
                            SocCode = socCode,
                            HlgtCode = hlgtCode
                        }
                    };
                }
                else
                {
                    return new ParseResult<SocHlgtRelation> { Success = false, Error = $"Error parsing file {FileName}: invalid number format" };
                }
            }
            else
            {
                return new ParseResult<SocHlgtRelation> { Success = false, Error = $"Error parsing file {FileName}: invalid line format" };
            }
        }
    }
}
