using System;
using System.Reactive;
using ReactiveUI;

namespace LinCon.Avalonia.ViewModels
{
  public class MenuViewModel : ReactiveObject, IRoutableViewModel
  {
    public IScreen HostScreen { get; }
    public string UrlPathSegment {get;} = Guid.NewGuid().ToString().Substring(0,5);
    public ReactiveCommand<Unit,IRoutableViewModel> GoCaseExportView {get;}
    public ReactiveCommand<Unit,IRoutableViewModel> GoCaseImportView {get;}
    public ReactiveCommand<Unit,IRoutableViewModel> GoCaseExplorerView {get;}


    public MenuViewModel(IScreen screen)
    {
      HostScreen = screen;

      GoCaseExportView = ReactiveCommand.CreateFromObservable(
        () => HostScreen.Router.Navigate.Execute(new CaseExportViewModel(HostScreen))
      );

      GoCaseImportView = ReactiveCommand.CreateFromObservable(
        () => HostScreen.Router.Navigate.Execute(new CaseImportViewModel(HostScreen))
      );

      GoCaseExplorerView = ReactiveCommand.CreateFromObservable(
        () => HostScreen.Router.Navigate.Execute(new CaseExplorerViewModel(HostScreen))
      );
    }
  }
}