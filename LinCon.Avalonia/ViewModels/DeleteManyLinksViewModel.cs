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
  public class DeleteManyLinksViewModel : ReactiveObject, IRoutableViewModel
  {
    public string UrlPathSegment {get; } = System.Guid.NewGuid ().ToString ().Substring (0, 5);
    public IScreen HostScreen {get;}

    ICaseRepository _caseRepository;

    public List<DeleteLinkItem> DeleteLinkItems {get;set;}

    IMapper _mapper;

    CaseViewModel _parentView;

    Case Case;

    public DeleteManyLinksViewModel(IScreen screen, CaseViewModel parentView, int caseId)
    {
        HostScreen = screen;
        _caseRepository = Locator.Current.GetService<ICaseRepository>();
        _mapper = Locator.Current.GetService<IMapper>();

        Case = _caseRepository.GetById(caseId);

        DeleteLinkItems = _mapper.Map<List<DeleteLinkItem>>(Case.Links);

        _parentView = parentView;

        ReturnCommand = ReactiveCommand.CreateFromTask(Return);
        DeleteManyCommand = ReactiveCommand.CreateFromTask(DeleteMany);
    }   

    public ReactiveCommand<Unit,Unit> ReturnCommand {get;}
    private Task<Unit> Return()
    {
      HostScreen.Router.NavigateBack.Execute();
      return Task.FromResult(Unit.Default);
    }

    public ReactiveCommand DeleteManyCommand {get;}
    private Task<Unit> DeleteMany()
    {
      var checkedItems = DeleteLinkItems.Where(x => x.IsChecked);
      var allLinks = Case.Links;
      var linksToNotDelete = allLinks.Where(x => !checkedItems.Any(y => y.Url == x.Url)).ToList(); 

      Case.Links = linksToNotDelete;
      _caseRepository.Update(Case);

      ReturnCommand.Execute();
      _parentView.RefreshCommand.Execute();
      return Task.FromResult(Unit.Default);
    }
  }
}