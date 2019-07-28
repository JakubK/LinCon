using System.Reactive;
using System.Threading.Tasks;
using LinCon.Core.Models;
using LinCon.Core.Services.Abstract;
using ReactiveUI;
using Splat;

namespace LinCon.Avalonia.ViewModels
{
  public class CaseViewModel : ReactiveObject, IRoutableViewModel
  {
    public string UrlPathSegment {get;set;}  = System.Guid.NewGuid ().ToString ().Substring (0, 5);

    public IScreen HostScreen {get;set;} 

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

        OpenLinkCommand = ReactiveCommand.CreateFromTask<string,Unit>(OpenLink);
        OpenAllLinksCommand = ReactiveCommand.CreateFromTask(OpenAllLinks);
        DeleteLinkCommand = ReactiveCommand.CreateFromTask<string,Unit>(DeleteLink);
    }

    public ReactiveCommand<string, Unit> OpenLinkCommand {get;}
    private Task<Unit> OpenLink(string link)
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

    public ReactiveCommand<string,Unit> DeleteLinkCommand {get;}
    private Task<Unit> DeleteLink(string link)
    {
      Case.Links.Remove(link);
      _caseRepository.Update(Case);
      Case = _caseRepository.GetById(Case.ID);
      return Task.FromResult(Unit.Default);
    }
  }
}