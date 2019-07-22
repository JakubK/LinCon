using System.Collections.Generic;
using LinCon.Core.Models;

namespace LinCon.Core.Services
{
    public interface ICaseRepository
    {
        IEnumerable<Case> GetAll();
        void Insert(Case @case);
    }
}