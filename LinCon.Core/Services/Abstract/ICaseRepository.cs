using System.Collections.Generic;
using LinCon.Core.Models;

namespace LinCon.Core.Services.Abstract
{
    public interface ICaseRepository
    {
        void Insert(Case @case);
        IEnumerable<Case> GetAll();
        void Update(Case @case);
        void Delete(int id);
    }
}