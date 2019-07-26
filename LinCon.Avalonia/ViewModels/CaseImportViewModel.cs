using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
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
      ImportCommand = ReactiveCommand.CreateFromTask(Import);
      
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

    public ReactiveCommand ImportCommand {get;}
    private async Task Import()
    {
      OpenFileDialog d = new OpenFileDialog();
      var result =  await d.ShowAsync(new Window());
      if(result != null)
      {
        var cases = _caseImporter.Import(result.ToList());
       _caseRepository.Insert(cases);
      }
    }
  }
}