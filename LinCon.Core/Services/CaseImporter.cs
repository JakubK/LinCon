using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using LinCon.Core.Models;
using LinCon.Core.Services.Abstract;
using Newtonsoft.Json;

namespace LinCon.Core.Services
{
  public class CaseImporter : ICaseImporter
  {
    public IEnumerable<Case> Import(params string[] paths)
    {
        foreach(var path in paths)
        {
            if(Path.GetExtension(path) == ".lc")
            {
              var cases = JsonConvert.DeserializeObject<Case[]>(string.Join(string.Empty,File.ReadAllLines(path)));
              foreach(var caseItem in cases)
                yield return caseItem;
            }
            else
            {
              Case c = new Case();
              c.Name = Path.GetFileNameWithoutExtension(path);
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
}