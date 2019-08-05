using System.Collections.Generic;
using System.IO;
using LinCon.Core.Models;
using LinCon.Core.Services.Abstract;
using Newtonsoft.Json;

namespace LinCon.Core.Services
{
  public class CaseExporter : ICaseExporter
  {
    public void Export(IEnumerable<Case> cases, string path)
    {
        File.WriteAllText(path, JsonConvert.SerializeObject(cases));
    }
  }
}