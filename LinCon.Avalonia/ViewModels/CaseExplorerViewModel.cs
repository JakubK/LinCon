using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using AutoMapper;
using LinCon.Avalonia.Models;
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

    private ObservableCollection<CaseItem> cases;
    public ObservableCollection<CaseItem> Cases
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

      Cases = _mapper.Map<ObservableCollection<CaseItem>> (_caseRepository.GetAll ());

      DeleteCommand = ReactiveCommand.CreateFromTask<CaseItem, Unit> (Delete);
      AddCommand = ReactiveCommand.CreateFromTask(Add);
      ExecuteCaseCommand = ReactiveCommand.CreateFromTask<int,Unit>(ExecuteCase);
      ViewCaseCommand = ReactiveCommand.CreateFromTask<int,Unit>(ViewCase);
      DeleteManyCasesCommand = ReactiveCommand.CreateFromTask(DeleteManyCases);
      EditCaseCommand = ReactiveCommand.CreateFromTask<int,Unit>(EditCase);
      ReturnCommand = ReactiveCommand.CreateFromTask(Return);
    }

    public ReactiveCommand ReturnCommand {get;}
    private Task<Unit> Return()
    {
      HostScreen.Router.NavigateBack.Execute();
      return Task.FromResult(Unit.Default);
    }

    public ReactiveCommand<int,Unit> EditCaseCommand {get;}
    private Task<Unit> EditCase(int caseId)
    {
      Router.Navigate.Execute(new EditCaseViewModel(this,this,caseId));
      return Task.FromResult(Unit.Default);
    }

    public ReactiveCommand<CaseItem, Unit> DeleteCommand { get; }
    private Task<Unit> Delete (CaseItem item) 
    {
      Router.Navigate.Execute(new DeleteCaseViewModel(this,this,item));
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
      HostScreen.Router.Navigate.Execute(new CaseViewModel(HostScreen,id));
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