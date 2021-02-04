using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Persistence
{
    public static class StringHelper
    {
        public static IEnumerable<string> SplitCSV(this string input)
        {
            var csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);

            foreach (Match match in csvSplit.Matches(input)) yield return match.Value.TrimStart(',');
        }
    }
}