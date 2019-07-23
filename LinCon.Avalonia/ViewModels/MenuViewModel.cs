using System;
using ReactiveUI;

namespace LinCon.Avalonia.ViewModels
{
  public class MenuViewModel : ReactiveObject, IRoutableViewModel
  {
    public IScreen HostScreen { get; }
    public string UrlPathSegment {get;} = Guid.NewGuid().ToString().Substring(0,5);

    public MenuViewModel(IScreen screen) => HostScreen = screen;
  }
}