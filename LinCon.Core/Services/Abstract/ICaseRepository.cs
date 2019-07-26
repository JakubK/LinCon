using System.Collections.Generic;
using LinCon.Core.Models;

namespace LinCon.Core.Services.Abstract
{
    public interface ICaseRepository
    {
        void Insert(Case @case);
        void Insert(IEnumerable<Case> cases);
        IEnumerable<Case> GetAll();
        void Update(Case @case);
        void Delete(int id);
    }
}