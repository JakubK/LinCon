using System.Collections.Generic;
using LinCon.Core.Models;
using LinCon.Core.Services.Abstract;
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

    public void Delete(int id)
    {
      _liteRepository.Delete<Case>(id);
    }

    public IEnumerable<Case> GetAll()
    {
      return _liteRepository.Query<Case>().ToList();
    }

    public Case GetById(int id)
    {
      return _liteRepository.Query<Case>().Where(x => x.ID == id).First();
    }

    public void Insert(Case @case)
    {
      _liteRepository.Insert<Case>(@case);
    }

    public void Insert(IEnumerable<Case> cases)
    {
      foreach(var @case in cases)
        Insert(@case);
    }

    public void Update(Case @case)
    {
      _liteRepository.Update<Case>(@case);
    }
  }
}