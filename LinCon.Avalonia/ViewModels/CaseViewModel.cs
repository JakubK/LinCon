using System.Reactive;
using System.Threading.Tasks;
using LinCon.Core.Models;
using LinCon.Core.Services.Abstract;
using ReactiveUI;
using Splat;

namespace LinCon.Avalonia.ViewModels
{
  public class CaseViewModel : ReactiveObject, IRoutableViewModel, IScreen
  {
    public string UrlPathSegment {get;set;}  = System.Guid.NewGuid ().ToString ().Substring (0, 5);

    public IScreen HostScreen {get;set;} 
    public RoutingState Router { get; } = new RoutingState();

    private Case @case;
    public Case Case
    {
      get => @case;
      set => this.RaiseAndSetIfChanged(ref @case,value);
    }
    
    ICaseRepository _caseRepository;
    ICaseProcessor _caseProcessor;
    public CaseViewModel(IScreen screen, int id)
    {
        HostScreen = screen;
      
        _caseRepository = Locator.Current.GetService<ICaseRepository>();
        _caseProcessor = Locator.Current.GetService<ICaseProcessor>();

        Case = _caseRepository.GetById(id);

        OpenLinkCommand = ReactiveCommand.CreateFromTask<Link,Unit>(OpenLink);
        OpenAllLinksCommand = ReactiveCommand.CreateFromTask(OpenAllLinks);
        DeleteLinkCommand = ReactiveCommand.CreateFromTask<Link,Unit>(DeleteLink);
        AddLinkCommand = ReactiveCommand.CreateFromTask(AddLink);
        RefreshCommand = ReactiveCommand.CreateFromTask<Unit>(Refresh);
        EditLinkCommand = ReactiveCommand.CreateFromTask<Link,Unit>(EditLink);
        DeleteManyLinksCommand = ReactiveCommand.CreateFromTask(DeleteManyLinks);
    }

    public ReactiveCommand<Link, Unit> OpenLinkCommand {get;}
    private Task<Unit> OpenLink(Link link)
    {
      _caseProcessor.ProcessLink(link);
      return Task.FromResult(Unit.Default);
    }

    public ReactiveCommand OpenAllLinksCommand {get;}
    private Task<Unit> OpenAllLinks()
    {
      _caseProcessor.ProcessCase(Case);
      return Task.FromResult(Unit.Default);
    }

    public ReactiveCommand<Link,Unit> DeleteLinkCommand {get;}
    private Task<Unit> DeleteLink(Link link)
    {
      Router.Navigate.Execute(new DeleteLinkViewModel(this,this,Case.ID, link));
      return Task.FromResult(Unit.Default);
    }

    public ReactiveCommand AddLinkCommand {get;}
    private Task<Unit> AddLink()
    {
      Router.Navigate.Execute(new AddLinkViewModel(this,this,Case.ID));
      return Task.FromResult(Unit.Default);
    }

    public ReactiveCommand<Unit,Unit> RefreshCommand {get;}
    private Task<Unit> Refresh()
    {
      Case = _caseRepository.GetById(Case.ID);      
      return Task.FromResult(Unit.Default);
    }

    public ReactiveCommand<Link,Unit> EditLinkCommand {get;}
    private Task<Unit> EditLink(Link link)
    {
      Router.Navigate.Execute(new EditLinkViewModel(this,this,Case.ID,link));
      return Task.FromResult(Unit.Default);
    }

    public ReactiveCommand DeleteManyLinksCommand {get;}
    private Task<Unit> DeleteManyLinks()
    {
      Router.Navigate.Execute(new DeleteManyLinksViewModel(this,this, Case.ID));
      return Task.FromResult(Unit.Default);
    }
  }
}