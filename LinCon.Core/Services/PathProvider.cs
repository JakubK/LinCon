using System;
using System.IO;
using LinCon.Core.Services.Abstract;

namespace LinCon.Core.Services
{
  public class PathProvider : IPathProvider
  {
    public string DbFileName  
    {
      get
      {
          var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Database.db");
          Directory.CreateDirectory(Path.GetDirectoryName(path));
          return path;
      }
    }
  }
}