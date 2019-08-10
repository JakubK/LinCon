using System.Reactive;
using System.Threading.Tasks;
using AutoMapper;
using LinCon.Avalonia.Models;
using LinCon.Core.Models;
using LinCon.Core.Services.Abstract;
using ReactiveUI;
using Splat;

namespace LinCon.Avalonia.ViewModels
{
  public class DeleteCaseViewModel : ReactiveObject, IRoutableViewModel
  {
    public string UrlPathSegment {get; } = System.Guid.NewGuid ().ToString ().Substring (0, 5);
    public IScreen HostScreen {get;}
    CaseExplorerViewModel _parent;
    public CaseItem DeleteItem {get;set;}
    ICaseRepository _caseRepository;

    public DeleteCaseViewModel(IScreen screen, CaseExplorerViewModel parent, CaseItem deleteItem)
    {
        HostScreen = screen;
        _parent = parent;

        _caseRepository = Locator.Current.GetService<ICaseRepository>();

        DeleteItem = deleteItem;

        DeleteCommand = ReactiveCommand.CreateFromTask(Delete);
        ReturnCommand = ReactiveCommand.CreateFromTask(Return);
    }   

    public ReactiveCommand DeleteCommand {get;}

    private Task<Unit> Delete()
    {
      _caseRepository.Delete(DeleteItem.ID);
      _parent.Cases.Remove(DeleteItem);

      ReturnCommand.Execute();
      return Task.FromResult(Unit.Default);
    }

    public ReactiveCommand<Unit,Unit> ReturnCommand {get;}
    private Task<Unit> Return()
    {
      HostScreen.Router.NavigateBack.Execute();
      return Task.FromResult(Unit.Default);
    }
  }
}