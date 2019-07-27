using System.Collections.Generic;
using System.Linq;
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
  public class CaseExplorerViewModel : ReactiveObject, IRoutableViewModel 
  {
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = System.Guid.NewGuid ().ToString ().Substring (0, 5);

    private IEnumerable<ExportItem> cases;
    public IEnumerable<ExportItem> Cases
    {
      get => cases;
      set => this.RaiseAndSetIfChanged(ref cases,value);
    }

    ICaseRepository _caseRepository;
    IMapper _mapper;

    public CaseExplorerViewModel (IScreen screen) 
    {
      HostScreen = screen;

      _caseRepository = Locator.Current.GetService<ICaseRepository> ();
      _mapper = Locator.Current.GetService<IMapper> ();

      Cases = _mapper.Map<ExportItem[]> (_caseRepository.GetAll ());

      DeleteCommand = ReactiveCommand.CreateFromTask<int, Unit> (Delete);
      AddCommand = ReactiveCommand.CreateFromTask(Add);
    }

    public ReactiveCommand<int, Unit> DeleteCommand { get; }
    private Task<Unit> Delete (int id) 
    {
      _caseRepository.Delete(id);
      Cases = _mapper.Map<List<ExportItem>> (_caseRepository.GetAll ());
      return Task.FromResult (Unit.Default);
    }

    public ReactiveCommand AddCommand {get;}
    private Task Add()
    {
      Case c = new Case();
      _caseRepository.Insert(c);
      Cases = _mapper.Map<List<ExportItem>> (_caseRepository.GetAll ());
      return Task.FromResult(0);
    }
  }
}