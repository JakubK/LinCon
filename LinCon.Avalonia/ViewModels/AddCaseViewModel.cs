using System.Reactive;
using System.Threading.Tasks;
using LinCon.Core.Models;
using LinCon.Core.Services.Abstract;
using ReactiveUI;
using Splat;

namespace LinCon.Avalonia.ViewModels
{
  public class AddCaseViewModel : ReactiveObject, IRoutableViewModel
  {
    public string UrlPathSegment {get; } = System.Guid.NewGuid ().ToString ().Substring (0, 5);

    public IScreen HostScreen {get;}

    public AddCaseViewModel(IScreen screen)
    {
        HostScreen = screen;
    }   
  }
}