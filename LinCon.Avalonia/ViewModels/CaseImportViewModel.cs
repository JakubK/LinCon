using ReactiveUI;

namespace LinCon.Avalonia.ViewModels
{
  public class CaseImportViewModel : ReactiveObject, IRoutableViewModel
  {
    public IScreen HostScreen { get; }
    public string UrlPathSegment {get;} = System.Guid.NewGuid().ToString().Substring(0,5);

    public CaseImportViewModel(IScreen screen) => HostScreen = screen;
  }
}