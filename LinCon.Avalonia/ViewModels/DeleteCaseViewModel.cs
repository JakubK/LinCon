using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
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
    public Case Case {get;set;}
    ICaseRepository _caseRepository;
    public DeleteCaseViewModel(IScreen screen, CaseExplorerViewModel parent, int caseId)
    {
        HostScreen = screen;
        _parent = parent;

        _caseRepository = Locator.Current.GetService<ICaseRepository>();

        Case = _caseRepository.GetById(caseId);

        DeleteCommand = ReactiveCommand.CreateFromTask(Delete);
        ReturnCommand = ReactiveCommand.CreateFromTask(Return);
    }   

    public ReactiveCommand DeleteCommand {get;}

    private Task<Unit> Delete()
    {
      _caseRepository.Delete(Case.ID);

      ReturnCommand.Execute();
      _parent.RefreshCommand.Execute();
      
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