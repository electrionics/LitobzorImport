using LitobzorImport.DataAccess.Contracts;
using LitobzorImport.Domain.References;
using System.Text.RegularExpressions;

namespace LitobzorImport.DataAccess.DomainConfig.References
{
    public class HighLevelGroupTermConfig : IDomainConfig<HighLevelGroupTerm>
    {
        public string FileName => "hlgt.asc";

        public string DisplayName => "HLGT";

        public string LongDisplayName => "High Level Group Terms";

        public ParseResult<HighLevelGroupTerm> ParseLine(string line)
        {
            var regex = new Regex("(\\d+)\\$(.+)\\${8}");
            var match = regex.Match(line);
            if (match != null && match.Groups.Count == 3)
            {
                if (long.TryParse(match.Groups[1].Value, out var code))
                {
                    return new ParseResult<HighLevelGroupTerm>
                    {
                        Success = true,
                        Value = new HighLevelGroupTerm
                        {
                            HlgtCode = code,
                            HlgtName = match.Groups[2].Value
                        }
                    };
                }
                else
                {
                    return new ParseResult<HighLevelGroupTerm> { Success = false, Error = $"Error parsing file {FileName}: invalid number format" };
                }
            }
            else
            {
                return new ParseResult<HighLevelGroupTerm> { Success = false, Error = $"Error parsing file {FileName}: invalid line format" };
            }
        }
    }
}
