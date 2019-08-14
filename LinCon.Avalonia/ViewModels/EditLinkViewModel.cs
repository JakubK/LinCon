using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using LinCon.Avalonia.Models;
using LinCon.Core.Models;
using LinCon.Core.Services.Abstract;
using ReactiveUI;
using Splat;

namespace LinCon.Avalonia.ViewModels
{
  public class EditLinkViewModel : ReactiveObject, IRoutableViewModel
  {
    public string UrlPathSegment {get; } = System.Guid.NewGuid ().ToString ().Substring (0, 5);

    public IScreen HostScreen {get;}

    private string name;
    public string Name
    {
      get => name;
      set => this.RaiseAndSetIfChanged(ref name, value);
    }

    private string url;
    public string Url
    {
      get => url;
      set => this.RaiseAndSetIfChanged(ref url, value);
    }

    ICaseRepository _caseRepository;

    private Case _case;
    public Case Case 
    {
      get => _case;
      set => this.RaiseAndSetIfChanged(ref _case, value);
    }

    CaseViewModel _parentViewModel;

    LinkItem oldLink;

    public EditLinkViewModel(IScreen screen, CaseViewModel parentViewModel, int caseId, LinkItem link)
    {
        HostScreen = screen;

        EditCommand = ReactiveCommand.CreateFromTask(Edit);
        ReturnCommand = ReactiveCommand.CreateFromTask(Return);

        _parentViewModel = parentViewModel;
        _caseRepository = Locator.Current.GetService<ICaseRepository>();

        Case = _caseRepository.GetById(caseId);

        Name = link.Name;
        Url = link.Url;
        oldLink = link;
    }   

    public ReactiveCommand EditCommand {get;}
    private Task<Unit> Edit()
    {
      var linkToUpdate = Case.Links.Where(x => x.Url == oldLink.Url).First();      
      var item = _parentViewModel.Links.First(x => x.Url == oldLink.Url && x.Name == oldLink.Name);
      item.Name = linkToUpdate.Name =  Name;
      item.Url = linkToUpdate.Url = Url;

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