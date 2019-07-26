using System.Collections.Generic;
using System.IO;
using System.Text;
using LinCon.Core.Models;
using LinCon.Core.Services.Abstract;

namespace LinCon.Core.Services
{
  public class CaseExporter : ICaseExporter
  {
    public void Export(IEnumerable<Case> cases, string path)
    {
        StringBuilder builder = new StringBuilder();
        foreach(var @case in cases)
        {
            foreach(var link in @case.Links)
            {
                builder.Append(link);
                builder.AppendLine();
            }
        }
        File.WriteAllText(path,builder.ToString());
    }
  }
}