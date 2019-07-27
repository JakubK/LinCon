using System.Collections.Generic;
using LinCon.Core.Models;

namespace LinCon.Core.Services.Abstract
{
    public interface ICaseRepository
    {
        void Insert(Case @case);
        void Insert(IEnumerable<Case> cases);
        IEnumerable<Case> GetAll();
        Case GetById(int id);
        void Update(Case @case);
        void Delete(int id);
    }
}