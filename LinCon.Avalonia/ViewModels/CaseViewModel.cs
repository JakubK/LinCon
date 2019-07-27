using ReactiveUI;

namespace LinCon.Avalonia.ViewModels
{
  public class CaseViewModel : ReactiveObject, IRoutableViewModel
  {
    public string UrlPathSegment {get;set;}  = System.Guid.NewGuid ().ToString ().Substring (0, 5);

    public IScreen HostScreen {get;set;} 

    public CaseViewModel(IScreen screen)
    {
        HostScreen = screen;
    }
  }
}