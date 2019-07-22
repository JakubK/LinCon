using System.Collections.Generic;
using LinCon.Core.Models;
using LiteDB;

namespace LinCon.Core.Services
{
  public class CaseRepository : ICaseRepository
  {
    LiteRepository _liteRepository;

    public CaseRepository()
    {
      _liteRepository = new LiteRepository("connectionString");
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