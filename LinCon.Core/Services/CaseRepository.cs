using System.Collections.Generic;
using LinCon.Core.Models;
using LiteDB;

namespace LinCon.Core.Services
{
  public class CaseRepository : ICaseRepository
  {
    LiteRepository _liteRepository;
    IPathProvider _pathProvider;

    public CaseRepository(IPathProvider pathProvider)
    {
      _pathProvider = pathProvider;
      _liteRepository = new LiteRepository($"Filename={_pathProvider.DbFileName};Mode=Exclusive");
    }
    public IEnumerable<Case> GetAll()
    {
      return _liteRepository.Query<Case>().ToList();
    }

    public void Insert(Case @case)
    {
      _liteRepository.Insert<Case>(@case);
    }
  }
}