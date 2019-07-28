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

    public void ProcessLink(Link link)
    {
      try
      {
          Process.Start(link.Url);
      }
      catch
      {
          if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
          {
              link.Url = link.Url.Replace("&", "^&");
              Process.Start(new ProcessStartInfo("cmd", $"/c start {link.Url}") { CreateNoWindow = true });
          }
          else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
          {
              Process.Start("xdg-open", link.Url);
          }
          else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
          {
              Process.Start("open", link.Url);
          }
          else
          {
              throw;
          }
      }
    }
  }
}