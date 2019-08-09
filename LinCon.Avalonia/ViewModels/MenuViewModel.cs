using System.Reactive;
using ReactiveUI;

namespace LinCon.Avalonia.ViewModels
{
  public class MenuViewModel : ReactiveObject, IRoutableViewModel
  {
    public IScreen HostScreen { get; }
    public string UrlPathSegment {get;}
    public ReactiveCommand<Unit,IRoutableViewModel> RouteCaseExport {get;}
    public ReactiveCommand<Unit,IRoutableViewModel> RouteCaseImport {get;}
    public ReactiveCommand<Unit,IRoutableViewModel> RouteCaseExplorer {get;}

    public MenuViewModel(IScreen screen)
    {
      HostScreen = screen;

      RouteCaseExport = ReactiveCommand.CreateFromObservable(
        () => HostScreen.Router.Navigate.Execute(new CaseExportViewModel(HostScreen))
      );

      RouteCaseImport = ReactiveCommand.CreateFromObservable(
        () => HostScreen.Router.Navigate.Execute(new CaseImportViewModel(HostScreen))
      );

      RouteCaseExplorer = ReactiveCommand.CreateFromObservable(
        () => HostScreen.Router.Navigate.Execute(new CaseExplorerViewModel(HostScreen))
      );
    }
  }
}