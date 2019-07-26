using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using ReactiveUI;

namespace LinCon.Avalonia.ViewModels
{
  public class CaseExportViewModel : ReactiveObject, IRoutableViewModel
  {
    public IScreen HostScreen { get; }
    public string UrlPathSegment {get;} = System.Guid.NewGuid().ToString().Substring(0,5);

    string exportPath;
    public string ExportPath
    {
      get => exportPath;
      set => this.RaiseAndSetIfChanged(ref exportPath, value);
    }
    public CaseExportViewModel(IScreen screen)
    {
      HostScreen = screen;
      OpenSaveFileDialogCommand = ReactiveCommand.CreateFromTask(OpenSaveFileDialog);
    }
    public ReactiveCommand OpenSaveFileDialogCommand {get;}
    private async Task OpenSaveFileDialog()
    {
      SaveFileDialog d = new SaveFileDialog();
      var result = await d.ShowAsync(new Window());
      if(result != null)
      {
        ExportPath = result;
      }      
    }
  }
}