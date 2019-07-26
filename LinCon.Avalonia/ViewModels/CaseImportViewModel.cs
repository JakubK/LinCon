using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Input;
using LinCon.Core.Services.Abstract;
using ReactiveUI;
using Splat;

namespace LinCon.Avalonia.ViewModels
{
  public class CaseImportViewModel : ReactiveObject, IRoutableViewModel
  {
    public IScreen HostScreen { get; }
    public string UrlPathSegment {get;} = System.Guid.NewGuid().ToString().Substring(0,5);

    ICaseRepository _caseRepository;
    ICaseImporter _caseImporter;

    public CaseImportViewModel(IScreen screen)
    { 
      HostScreen = screen;
      DropCommand = ReactiveCommand.CreateFromTask<DragEventArgs>(Drop);
      
      _caseRepository = Locator.Current.GetService<ICaseRepository>();
      _caseImporter = Locator.Current.GetService<ICaseImporter>();
    }
    public ReactiveCommand<DragEventArgs,Unit> DropCommand { get; }
    private Task Drop(DragEventArgs e)
    {
      var cases = _caseImporter.Import(e.Data.GetFileNames());
      _caseRepository.Insert(cases);
      return Task.FromResult(0);
    }
  }
}