using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Input;
using ReactiveUI;

namespace LinCon.Avalonia.ViewModels
{
  public class CaseImportViewModel : ReactiveObject, IRoutableViewModel
  {
    public IScreen HostScreen { get; }
    public string UrlPathSegment {get;} = System.Guid.NewGuid().ToString().Substring(0,5);
    public CaseImportViewModel(IScreen screen)
    { 
      HostScreen = screen;
      DropCommand = ReactiveCommand.CreateFromTask<DragEventArgs>(Drop);
   
    }
    public ReactiveCommand<DragEventArgs,Unit> DropCommand { get; }
    private Task Drop(DragEventArgs e)
    {
      
      return Task.FromResult(0);
    }
  }
}