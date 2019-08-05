using System.Reactive;
using System.Threading.Tasks;
using LinCon.Core.Models;
using LinCon.Core.Services.Abstract;
using ReactiveUI;
using Splat;

namespace LinCon.Avalonia.ViewModels
{
  public class EditCaseViewModel : ReactiveObject, IRoutableViewModel
  {
    public string UrlPathSegment {get; } = System.Guid.NewGuid ().ToString ().Substring (0, 5);
    public IScreen HostScreen {get;}

    private string name;
    public string Name
    {
      get => name;
      set => this.RaiseAndSetIfChanged(ref name,value);
    }

    CaseExplorerViewModel _parent;

    ICaseRepository _caseRepository;

    Case Case;
    public EditCaseViewModel(IScreen screen, CaseExplorerViewModel parent, int caseId)
    {
        HostScreen = screen;
        _parent = parent;

        _caseRepository = Locator.Current.GetService<ICaseRepository>();
        Case = _caseRepository.GetById(caseId);
        Name = Case.Name;

        EditCommand = ReactiveCommand.CreateFromTask(Edit);
        ReturnCommand = ReactiveCommand.CreateFromTask(Return);
    }   

    public ReactiveCommand EditCommand {get;}
    private Task<Unit> Edit()
    {
      Case.Name = Name;
      _caseRepository.Update(Case);
      
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