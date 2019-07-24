using ReactiveUI;

namespace LinCon.Avalonia.ViewModels
{
  public class CaseExportViewModel : ReactiveObject, IRoutableViewModel
  {
    public IScreen HostScreen { get; }
    public string UrlPathSegment {get;} = System.Guid.NewGuid().ToString().Substring(0,5);

    public CaseExportViewModel(IScreen screen) => HostScreen = screen;
  }
}