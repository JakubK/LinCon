using LinCon.Core.Models;

namespace LinCon.Core.Services.Abstract
{
    public interface ICaseProcessor
    {
        void ProcessCase(Case @case);
        void ProcessLink(string link);
    }
}