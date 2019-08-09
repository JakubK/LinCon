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
      ProcessUrl(link.Url);
    }

    public void ProcessUrl(string url)
    {
      try
      {
          Process.Start(url);
      }
      catch
      {
          if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
          {
              url = url.Replace("&", "^&");
              Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
          }
          else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
          {
              Process.Start("xdg-open", url);
          }
          else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
          {
              Process.Start("open",url);
          }
          else
          {
              throw;
          }
      }
    }
  }
}