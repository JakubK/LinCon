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
  public class AddLinkViewModel : ReactiveObject, IRoutableViewModel
  {
    public string UrlPathSegment {get; } = System.Guid.NewGuid ().ToString ().Substring (0, 5);
    public IScreen HostScreen {get;}

    private string name;
    public string Name
    {
      get => name;
      set => this.RaiseAndSetIfChanged(ref name, value);
    }

    private string link;
    public string Link
    {
      get => link;
      set => this.RaiseAndSetIfChanged(ref link, value);
    }

    ICaseRepository _caseRepository;

    private Case _case;
    public Case Case 
    {
      get => _case;
      set => this.RaiseAndSetIfChanged(ref _case, value);
    }

    CaseViewModel _parentViewModel;

    IMapper _mapper;

    public AddLinkViewModel(IScreen screen, CaseViewModel parentViewModel, int caseId)
    {
        HostScreen = screen;

        AddLinkCommand = ReactiveCommand.CreateFromTask(AddLink);
        ReturnCommand = ReactiveCommand.CreateFromTask(Return);

        _parentViewModel = parentViewModel;
        _caseRepository = Locator.Current.GetService<ICaseRepository>();
        _mapper = Locator.Current.GetService<IMapper>();

        Case = _caseRepository.GetById(caseId);
    }   

    public ReactiveCommand AddLinkCommand {get;}
    private Task<Unit> AddLink()
    {
      Link link =new Link
      {
        Name = Name,
        Url = Link
      }; 

      _parentViewModel.Links.Add(_mapper.Map<LinkItem>(link));

      Case.Links.Add(link);
      _caseRepository.Update(Case);

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