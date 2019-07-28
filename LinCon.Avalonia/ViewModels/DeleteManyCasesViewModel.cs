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
  public class DeleteManyCasesViewModel : ReactiveObject, IRoutableViewModel
  {
    public string UrlPathSegment {get; } = System.Guid.NewGuid ().ToString ().Substring (0, 5);
    public IScreen HostScreen {get;}

    public List<ExportItem> CaseItems {get;set;}

    ICaseRepository _caseRepository;
    IMapper _mapper;

    public DeleteManyCasesViewModel(IScreen screen)
    {
      HostScreen = screen;

      _caseRepository = Locator.Current.GetService<ICaseRepository>();
      _mapper = Locator.Current.GetService<IMapper>();

      CaseItems = _mapper.Map<List<ExportItem>>(_caseRepository.GetAll());
    }   
  }
}