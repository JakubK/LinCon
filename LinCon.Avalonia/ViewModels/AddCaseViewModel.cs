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

    IMapper _mapper;

    public AddCaseViewModel(IScreen screen, CaseExplorerViewModel parent)
    {
        HostScreen = screen;
        _parent = parent;
        _mapper = Locator.Current.GetService<IMapper>();

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
      Case c = new Case{
        Name = Name
      };

      _caseRepository.Insert(c);
      _parent.Cases.Add(_mapper.Map<ExportItem>(c));

      ReturnCommand.Execute();
      return Task.FromResult(Unit.Default);
    }
  }
}