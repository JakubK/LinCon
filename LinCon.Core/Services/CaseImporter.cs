using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using LinCon.Core.Models;
using LinCon.Core.Services.Abstract;

namespace LinCon.Core.Services
{
  public class CaseImporter : ICaseImporter
  {
    public IEnumerable<Case> Import(IEnumerable<string> paths)
    {
        foreach(var path in paths)
        {
            Case c = new Case();
            string text = string.Join("\n",File.ReadAllLines(path));
            var linkParser = new Regex(@"\b(?:https?://|www\.)[^ \f\n\r\t\v\]]+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach(Match m in linkParser.Matches(text))
            {
                c.Links.Add(new Link
                {
                  Name = "",
                  Url = m.Value
                });
            }

            yield return c;
        }
    }
  }
}