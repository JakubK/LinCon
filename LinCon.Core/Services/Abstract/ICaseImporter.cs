using System.Collections.Generic;
using LinCon.Core.Models;

namespace LinCon.Core.Services.Abstract
{
    public interface ICaseImporter
    {
        IEnumerable<Case> Import(IEnumerable<string> paths);
    }
}