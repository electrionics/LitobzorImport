using LitobzorImport.DataAccess.Contracts;
using LitobzorImport.Domain.Aggregates;
using System.Text.RegularExpressions;

namespace LitobzorImport.DataAccess.DomainConfig.Aggregate
{
    public class MedDraHierarchyConfig : IDomainConfig<MedDraHierarchy>
    {
        public string FileName => "mdhier.asc";

        public string DisplayName => "MDHIER";

        public string LongDisplayName => "Hierarchy records";

        public ParseResult<MedDraHierarchy> ParseLine(string line)
        {
            var regex = new Regex("(\\d+)\\$(\\d+)\\$(\\d+)\\$(\\d+)\\$(.+)\\$(.+)\\$(.+)\\$(.+)\\$(.+)\\${2}(\\d+)\\$([YN])\\$");
            var match = regex.Match(line);
            if (match != null && match.Groups.Count == 12)
            {
                if (long.TryParse(match.Groups[1].Value, out var ptCode) &&
                    long.TryParse(match.Groups[2].Value, out var hltCode) &&
                    long.TryParse(match.Groups[3].Value, out var hlgtCode) &&
                    long.TryParse(match.Groups[4].Value, out var socCode) &&
                    long.TryParse(match.Groups[10].Value, out var ptSocCode))
                {
                    return new ParseResult<MedDraHierarchy>
                    {
                        Success = true,
                        Value = new MedDraHierarchy
                        {
                            PtCode = ptCode,
                            HltCode = hltCode,
                            HlgtCode = hlgtCode,
                            SocCode = socCode,

                            PtName = match.Groups[5].Value,
                            HltName = match.Groups[6].Value,
                            HlgtName = match.Groups[7].Value,
                            SocName = match.Groups[8].Value,
                            SocAbbrev = match.Groups[9].Value,

                            PtSocCode = ptSocCode,
                            PrimarySocFg = match.Groups[11].Value[0]
                        }
                    };
                }
                else
                {
                    return new ParseResult<MedDraHierarchy> { Success = false, Error = $"Error parsing file {FileName}: invalid number format" };
                }
            }
            else
            {
                return new ParseResult<MedDraHierarchy> { Success = false, Error = $"Error parsing file {FileName}: invalid line format" };
            }
        }
    }
}
