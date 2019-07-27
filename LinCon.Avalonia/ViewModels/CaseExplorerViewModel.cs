using System.Collections.Generic;
using AutoMapper;
using LinCon.Avalonia.Models;
using LinCon.Core.Services.Abstract;
using ReactiveUI;
using Splat;

namespace LinCon.Avalonia.ViewModels
{
  public class CaseExplorerViewModel : ReactiveObject, IRoutableViewModel
  {
    public IScreen HostScreen { get; }
    public string UrlPathSegment {get;} = System.Guid.NewGuid().ToString().Substring(0,5);
    public IEnumerable<ExportItem> Cases { get;set; }

    ICaseRepository _caseRepository;
    IMapper _mapper;

    public CaseExplorerViewModel(IScreen screen)
    {
      HostScreen = screen;
      _caseRepository = Locator.Current.GetService<ICaseRepository>();
      _mapper = Locator.Current.GetService<IMapper>();

      Cases = _mapper.Map<ExportItem[]>(_caseRepository.GetAll());
    }
  }
}