using LinCon.Core.Services.Abstract;

namespace LinCon.Core.Services
{
  public class PathProvider : IPathProvider
  {
    public string DbFileName => "Database.db";
  }
}