using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using LinCon.Core.Services.Abstract;
using ReactiveUI;
using Splat;

namespace LinCon.Avalonia.ViewModels
{
  public class CaseExportViewModel : ReactiveObject, IRoutableViewModel
  {
    public CaseExportViewModel(IScreen hostScreen, string urlPathSegment, ReactiveCommand openSaveFileDialogCommand, ReactiveCommand exportCasesCommand) 
        {
          this.HostScreen = hostScreen;
              this.UrlPathSegment = urlPathSegment;
              this.OpenSaveFileDialogCommand = openSaveFileDialogCommand;
              this.ExportCasesCommand = exportCasesCommand;
               
        }
            public IScreen HostScreen { get; }
    public string UrlPathSegment {get;} = System.Guid.NewGuid().ToString().Substring(0,5);

    string exportPath;
    public string ExportPath
    {
      get => exportPath;
      set => this.RaiseAndSetIfChanged(ref exportPath, value);
    }

    ICaseExporter _caseExporter;

    public CaseExportViewModel(IScreen screen)
    {
      HostScreen = screen;
      OpenSaveFileDialogCommand = ReactiveCommand.CreateFromTask(OpenSaveFileDialog);
      ExportCasesCommand = ReactiveCommand.CreateFromTask(ExportCases);

      _caseExporter = Locator.Current.GetService<ICaseExporter>();
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

    public ReactiveCommand ExportCasesCommand {get;}
    private Task ExportCases()
    {
      _caseExporter.Export(null,ExportPath);
      return Task.FromResult(0);
    }
  }
}