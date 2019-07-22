using System.Diagnostics;
using System.Runtime.InteropServices;
using LinCon.Core.Models;
using LinCon.Core.Services.Abstract;

namespace LinCon.Core.Services
{
  public class CaseProcessor : ICaseProcessor
  {
    public void ProcessCase(Case @case)
    {
      foreach(var link in @case.Links)
        ProcessLink(link);
    }

    void ProcessLink(string link)
    {
      try
      {
          Process.Start(link);
      }
      catch
      {
          if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
          {
              link = link.Replace("&", "^&");
              Process.Start(new ProcessStartInfo("cmd", $"/c start {link}") { CreateNoWindow = true });
          }
          else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
          {
              Process.Start("xdg-open", link);
          }
          else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
          {
              Process.Start("open", link);
          }
          else
          {
              throw;
          }
      }
    }
  }
}