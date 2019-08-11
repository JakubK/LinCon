using System.Collections.ObjectModel;
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
  public class CaseViewModel : ReactiveObject, IRoutableViewModel, IScreen
  {
    public string UrlPathSegment {get;set;}  = System.Guid.NewGuid ().ToString ().Substring (0, 5);

    public IScreen HostScreen {get;set;} 
    public RoutingState Router { get; } = new RoutingState();

    public ObservableCollection<LinkItem> Links {get;set;}
    
    ICaseRepository _caseRepository;
    ICaseProcessor _caseProcessor;
    IMapper _mapper;

    int caseId;
    public string Name {get;set;}
    public CaseViewModel(IScreen screen, int id)
    {
        HostScreen = screen;
      
        _caseRepository = Locator.Current.GetService<ICaseRepository>();
        _caseProcessor = Locator.Current.GetService<ICaseProcessor>();
        _mapper = Locator.Current.GetService<IMapper>();

        caseId = id;
        Name = _caseRepository.GetById(caseId).Name;

        Links = _mapper.Map<ObservableCollection<LinkItem>>(_caseRepository.GetById(id).Links);

        OpenLinkCommand = ReactiveCommand.CreateFromTask<Link,Unit>(OpenLink);
        OpenAllLinksCommand = ReactiveCommand.CreateFromTask(OpenAllLinks);
        DeleteLinkCommand = ReactiveCommand.CreateFromTask<LinkItem,Unit>(DeleteLink);
        AddLinkCommand = ReactiveCommand.CreateFromTask(AddLink);
        EditLinkCommand = ReactiveCommand.CreateFromTask<LinkItem,Unit>(EditLink);
        DeleteManyLinksCommand = ReactiveCommand.CreateFromTask(DeleteManyLinks);
        ReturnCommand = ReactiveCommand.CreateFromTask(Return);
    }
    public ReactiveCommand<Unit,Unit> ReturnCommand {get;}
    private Task<Unit> Return()
    {
      HostScreen.Router.NavigateBack.Execute();
      return Task.FromResult(Unit.Default);
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
      foreach(var link in Links)
        _caseProcessor.ProcessUrl(link.Url);

      return Task.FromResult(Unit.Default);
    }

    public ReactiveCommand<LinkItem,Unit> DeleteLinkCommand {get;}
    private Task<Unit> DeleteLink(LinkItem link)
    {
      Router.Navigate.Execute(new DeleteLinkViewModel(this,this,caseId, link));
      return Task.FromResult(Unit.Default);
    }

    public ReactiveCommand AddLinkCommand {get;}
    private Task<Unit> AddLink()
    {
      Router.Navigate.Execute(new AddLinkViewModel(this,this,caseId));
      return Task.FromResult(Unit.Default);
    }

    public ReactiveCommand<LinkItem,Unit> EditLinkCommand {get;}
    private Task<Unit> EditLink(LinkItem link)
    {
      Router.Navigate.Execute(new EditLinkViewModel(this,this,caseId,link));
      return Task.FromResult(Unit.Default);
    }

    public ReactiveCommand DeleteManyLinksCommand {get;}
    private Task<Unit> DeleteManyLinks()
    {
      Router.Navigate.Execute(new DeleteManyLinksViewModel(this,this, caseId));
      return Task.FromResult(Unit.Default);
    }
  }
}