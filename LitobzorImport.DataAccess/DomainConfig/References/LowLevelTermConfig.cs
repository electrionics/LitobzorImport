using LitobzorImport.DataAccess.Contracts;
using LitobzorImport.Domain.References;
using System.Text.RegularExpressions;

namespace LitobzorImport.DataAccess.DomainConfig.References
{
    /// <summary>
    /// LLT
    /// </summary>
    public class LowLevelTermConfig : IDomainConfig<LowLevelTerm>
    {
        public string FileName => "llt.asc";

        public string DisplayName => "LLT";

        public string LongDisplayName => "Low Level Terms";

        public ParseResult<LowLevelTerm> ParseLine(string line)
        {
            var regex = new Regex("(\\d+)\\$(.+)\\$(\\d+)\\${7}([YN])\\${2}");
            var match = regex.Match(line);
            if (match != null && match.Groups.Count == 5)
            {
                if (long.TryParse(match.Groups[1].Value, out var code) && long.TryParse(match.Groups[3].Value, out var ptCode))
                {
                    return new ParseResult<LowLevelTerm>
                    {
                        Success = true,
                        Value = new LowLevelTerm
                        {
                            LltCode = code,
                            LltName = match.Groups[2].Value,
                            PtCode = ptCode,
                            LltCurrency = match.Groups[4].Value[0]
                        }
                    };
                }
                else
                {
                    return new ParseResult<LowLevelTerm> { Success = false, Error = $"Error parsing file {FileName}: invalid number format" };
                }
            }
            else
            {
                return new ParseResult<LowLevelTerm> { Success = false, Error = $"Error parsing file {FileName}: invalid line format" };
            }
        }
    }
}
