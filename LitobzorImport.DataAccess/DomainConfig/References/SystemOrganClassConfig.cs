using LitobzorImport.DataAccess.Contracts;
using LitobzorImport.Domain.References;
using System.Text.RegularExpressions;

namespace LitobzorImport.DataAccess.DomainConfig.References
{
    public class SystemOrganClassConfig : IDomainConfig<SystemOrganClass>
    {
        public string FileName => "soc.asc";

        public string DisplayName => "SOC";

        public string LongDisplayName => "System Organ Classes";

        public ParseResult<SystemOrganClass> ParseLine(string line)
        {
            var regex = new Regex("(\\d+)\\$(.+)\\$(.+)\\${8}");
            var match = regex.Match(line);
            if (match != null && match.Groups.Count == 4)
            {
                if (long.TryParse(match.Groups[1].Value, out var code))
                {
                    return new ParseResult<SystemOrganClass>
                    {
                        Success = true,
                        Value = new SystemOrganClass
                        {
                            SocCode = code,
                            SocName = match.Groups[2].Value,
                            SocAbbrev = match.Groups[3].Value
                        }
                    };
                }
                else
                {
                    return new ParseResult<SystemOrganClass> { Success = false, Error = $"Error parsing file {FileName}: invalid number format" };
                }
            }
            else
            {
                return new ParseResult<SystemOrganClass> { Success = false, Error = $"Error parsing file {FileName}: invalid line format" };
            }
        }
    }
}
