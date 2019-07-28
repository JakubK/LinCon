using System.Collections.Generic;
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
  public class CaseExplorerViewModel : ReactiveObject, IRoutableViewModel, IScreen
  {
    public RoutingState Router { get; } = new RoutingState();
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
    ICaseProcessor _caseProcessor;

    public CaseExplorerViewModel (IScreen screen) 
    {
      HostScreen = screen;

      _caseRepository = Locator.Current.GetService<ICaseRepository> ();
      _mapper = Locator.Current.GetService<IMapper> ();
      _caseProcessor = Locator.Current.GetService<ICaseProcessor>();

      Cases = _mapper.Map<ExportItem[]> (_caseRepository.GetAll ());

      DeleteCommand = ReactiveCommand.CreateFromTask<int, Unit> (Delete);
      AddCommand = ReactiveCommand.CreateFromTask(Add);
      ExecuteCaseCommand = ReactiveCommand.CreateFromTask<int,Unit>(ExecuteCase);
      ViewCaseCommand = ReactiveCommand.CreateFromTask<int,Unit>(ViewCase);
      DeleteManyCasesCommand = ReactiveCommand.CreateFromTask(DeleteManyCases);
      RefreshCommand = ReactiveCommand.CreateFromTask(Refresh);
      EditCaseCommand = ReactiveCommand.CreateFromTask<int,Unit>(EditCase);
    }

    public ReactiveCommand<int,Unit> EditCaseCommand {get;}
    private Task<Unit> EditCase(int caseId)
    {
      Router.Navigate.Execute(new EditCaseViewModel(this,this,caseId));
      return Task.FromResult(Unit.Default);
    }

    public ReactiveCommand<Unit,Unit> RefreshCommand {get;}
    private Task<Unit> Refresh()
    {
      Cases = _mapper.Map<ExportItem[]>(_caseRepository.GetAll());
      return Task.FromResult(Unit.Default);
    }

    public ReactiveCommand<int, Unit> DeleteCommand { get; }
    private Task<Unit> Delete (int id) 
    {
      Router.Navigate.Execute(new DeleteCaseViewModel(this,this,id));
      return Task.FromResult (Unit.Default);
    }

    public ReactiveCommand AddCommand {get;}
    private Task Add()
    {
      Router.Navigate.Execute(new AddCaseViewModel(this,this));
      return Task.FromResult(0);
    }

    public ReactiveCommand<int,Unit> ExecuteCaseCommand {get;}
    private Task<Unit> ExecuteCase(int id)
    {
      _caseProcessor.ProcessCase(_caseRepository.GetById(id));
      return Task.FromResult(Unit.Default);
    }

    public ReactiveCommand<int,Unit> ViewCaseCommand {get;}
    private Task<Unit> ViewCase(int id)
    {
      HostScreen.Router.Navigate.Execute(new CaseViewModel(this,id));
      return Task.FromResult(Unit.Default);
    }

    public ReactiveCommand DeleteManyCasesCommand {get;}
    private Task<Unit> DeleteManyCases()
    {
      Router.Navigate.Execute(new DeleteManyCasesViewModel(this,this));
      return Task.FromResult(Unit.Default);
    }
  }
}