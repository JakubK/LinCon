using System.Collections.Generic;
using LinCon.Core.Models;

namespace LinCon.Core.Services.Abstract
{
    public interface ICaseExporter
    {
        void Export(IEnumerable<Case> cases, string path);
    }
}