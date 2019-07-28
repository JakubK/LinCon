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
    CaseExplorerViewModel _parent;

    string name;
    public string Name 
    {
      get => name;
      set => this.RaiseAndSetIfChanged(ref name, value);
    }

    ICaseRepository _caseRepository;

    public AddCaseViewModel(IScreen screen, CaseExplorerViewModel parent)
    {
        HostScreen = screen;
        _parent = parent;

        Name = "New Empty Case";

        ReturnCommand = ReactiveCommand.CreateFromTask(Return);
        AddCommand = ReactiveCommand.CreateFromTask(Add);

        _caseRepository = Locator.Current.GetService<ICaseRepository>();
    }   

    public ReactiveCommand<Unit,Unit> ReturnCommand {get;}
    private Task<Unit> Return()
    {
      HostScreen.Router.NavigateBack.Execute();

      return Task.FromResult(Unit.Default);
    }

    public ReactiveCommand AddCommand {get;}
    private Task<Unit> Add()
    {
      _caseRepository.Insert(new Case
      {
        Name = Name
      });
      ReturnCommand.Execute();
      _parent.RefreshCommand.Execute();
      return Task.FromResult(Unit.Default);
    }
  }
}