using LitobzorImport.DataAccess.Contracts;
using LitobzorImport.Domain.References;
using System.Text.RegularExpressions;

namespace LitobzorImport.DataAccess.DomainConfig.References
{
    public class HighLevelTermConfig : IDomainConfig<HighLevelTerm>
    {
        public string FileName => "hlt.asc";

        public string DisplayName => "HLT";

        public string LongDisplayName => "High Level Terms";

        public ParseResult<HighLevelTerm> ParseLine(string line)
        {
            var regex = new Regex("(\\d+)\\$(.+)\\${8}");
            var match = regex.Match(line);
            if (match != null && match.Groups.Count == 3)
            {
                if (long.TryParse(match.Groups[1].Value, out var code))
                {
                    return new ParseResult<HighLevelTerm>
                    {
                        Success = true,
                        Value = new HighLevelTerm
                        {
                            HltCode = code,
                            HltName = match.Groups[2].Value
                        }
                    };
                }
                else
                {
                    return new ParseResult<HighLevelTerm> { Success = false, Error = $"Error parsing file {FileName}: invalid number format" };
                }
            }
            else
            {
                return new ParseResult<HighLevelTerm> { Success = false, Error = $"Error parsing file {FileName}: invalid line format" };
            }
        }
    }
}
