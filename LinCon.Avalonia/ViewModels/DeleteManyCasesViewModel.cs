using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using AutoMapper;
using LinCon.Avalonia.Models;
using LinCon.Core.Services.Abstract;
using ReactiveUI;
using Splat;

namespace LinCon.Avalonia.ViewModels
{
  public class DeleteManyCasesViewModel : ReactiveObject, IRoutableViewModel
  {
    public string UrlPathSegment {get; } = System.Guid.NewGuid ().ToString ().Substring (0, 5);
    public IScreen HostScreen {get;}
    public List<ExportItem> CaseItems {get;set;}

    ICaseRepository _caseRepository;
    IMapper _mapper;

    CaseExplorerViewModel _parent;

    public DeleteManyCasesViewModel(IScreen screen, CaseExplorerViewModel parent)
    {
      HostScreen = screen;

      _caseRepository = Locator.Current.GetService<ICaseRepository>();
      _mapper = Locator.Current.GetService<IMapper>();

      _parent = parent;

      CaseItems = _mapper.Map<List<ExportItem>>(_caseRepository.GetAll());

      ReturnCommand = ReactiveCommand.CreateFromTask(Return);
      DeleteManyCommand = ReactiveCommand.CreateFromTask(DeleteMany);
    }   

    public ReactiveCommand<Unit,Unit> ReturnCommand {get;}
    private Task<Unit> Return()
    {
      HostScreen.Router.NavigateBack.Execute();
      return Task.FromResult(Unit.Default);
    }

    public ReactiveCommand DeleteManyCommand {get;set;}
    private Task<Unit> DeleteMany()
    {
      var checkedItems = CaseItems.Where(x => x.IsChecked);
      var items = _parent.Cases.Where(x => checkedItems.Any(y => y.ID == x.ID));

      foreach(var item in items.ToList())
      {
        _parent.Cases.Remove(item);
      }

      foreach(var caseItem in checkedItems)
      {
        _caseRepository.Delete(caseItem.ID);
      }

      ReturnCommand.Execute();
      
      return Task.FromResult(Unit.Default);
    }
  }
}