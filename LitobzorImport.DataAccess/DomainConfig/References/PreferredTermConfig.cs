using LitobzorImport.DataAccess.Contracts;
using LitobzorImport.Domain.References;
using System.Text.RegularExpressions;

namespace LitobzorImport.DataAccess.DomainConfig.References
{
    public class PreferredTermConfig : IDomainConfig<PreferredTerm>
    {
        public string FileName => "pt.asc";

        public string DisplayName => "PT";

        public string LongDisplayName => "Preferred Terms";

        public ParseResult<PreferredTerm> ParseLine(string line)
        {
            var regex = new Regex("(\\d+)\\$(.+)\\${2}(\\d+)\\${8}");
            var match = regex.Match(line);
            if (match != null && match.Groups.Count == 4)
            {
                if (long.TryParse(match.Groups[1].Value, out var code) && long.TryParse(match.Groups[3].Value, out var socCode))
                {
                    return new ParseResult<PreferredTerm>
                    {
                        Success = true,
                        Value = new PreferredTerm
                        {
                            PtCode = code,
                            PtName = match.Groups[2].Value,
                            PtSocCode = socCode
                        }
                    };
                }
                else
                {
                    return new ParseResult<PreferredTerm> { Success = false, Error = $"Error parsing file {FileName}: invalid number format" };
                }
            }
            else
            {
                return new ParseResult<PreferredTerm> { Success = false, Error = $"Error parsing file {FileName}: invalid line format" };
            }
        }
    }
}
