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

    public Case Case {get;set;}
    
    ICaseRepository _caseRepository;
    ICaseProcessor _caseProcessor;
    public CaseViewModel(IScreen screen, int id)
    {
        HostScreen = screen;
        
        _caseRepository = Locator.Current.GetService<ICaseRepository>();
        _caseProcessor = Locator.Current.GetService<ICaseProcessor>();

        Case = _caseRepository.GetById(id);

        OpenLinkCommand = ReactiveCommand.CreateFromTask<string,Unit>(OpenLink);
    }

    public ReactiveCommand<string, Unit> OpenLinkCommand {get;}
    private Task<Unit> OpenLink(string link)
    {
      _caseProcessor.ProcessLink(link);
      return Task.FromResult(Unit.Default);
    }
  }
}