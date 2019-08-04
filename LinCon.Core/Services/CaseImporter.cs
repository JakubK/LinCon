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
            c.Name = Path.GetFileName(path);
            string text = string.Join("\n",File.ReadAllLines(path));
            var linkParser = new Regex(@"\b(?:https?://|www\.)[^ \f\n\r\t\v\]]+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var matches = linkParser.Matches(text);
            for(int i = 0;i < matches.Count;i++)
            {
              c.Links.Add(new Link
              {
                Url = matches[i].Value,
                Name= "Url " + i
              });
            }

            yield return c;
        }
    }
  }
}