using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using LinCon.Core.Models;
using LinCon.Core.Services.Abstract;
using ReactiveUI;
using Splat;

namespace LinCon.Avalonia.ViewModels
{
  public class DeleteLinkViewModel : ReactiveObject, IRoutableViewModel
  {
    public string UrlPathSegment {get; } = System.Guid.NewGuid ().ToString ().Substring (0, 5);
    public IScreen HostScreen {get;}
    ICaseRepository _caseRepository;

    private Case _case;
    public Case Case 
    {
      get => _case;
      set => this.RaiseAndSetIfChanged(ref _case, value);
    }

    public Link Link {get;set;}

    CaseViewModel _parentViewModel;

    public DeleteLinkViewModel(IScreen screen, CaseViewModel parentViewModel, int caseId, Link link)
    {
        HostScreen = screen;

        ReturnCommand = ReactiveCommand.CreateFromTask(Return);
        DeleteCommand = ReactiveCommand.CreateFromTask(Delete);

        _parentViewModel = parentViewModel;
        _caseRepository = Locator.Current.GetService<ICaseRepository>();

        Case = _caseRepository.GetById(caseId);
        Link = link;
    }   

    public ReactiveCommand DeleteCommand {get;}

    private Task<Unit> Delete()
    {
      var linkToRemove = Case.Links.Where(x => x.Url == Link.Url && x.Name == Link.Name).First();
      Case.Links.Remove(linkToRemove);
      _caseRepository.Update(Case);
      _parentViewModel.Links.Remove(Link);

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