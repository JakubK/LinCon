using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using AutoMapper;
using Avalonia.Controls;
using LinCon.Avalonia.Models;
using LinCon.Core.Models;
using LinCon.Core.Services.Abstract;
using ReactiveUI;
using Splat;

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

    public IEnumerable<ExportItem> Cases { get;set; }

    ICaseExporter _caseExporter;
    ICaseRepository _caseRepository;
    IMapper _mapper;

    public CaseExportViewModel(IScreen screen)
    {
      HostScreen = screen;
      OpenSaveFileDialogCommand = ReactiveCommand.CreateFromTask(OpenSaveFileDialog);
      ExportCasesCommand = ReactiveCommand.CreateFromTask(ExportCases);
      ReturnCommand = ReactiveCommand.CreateFromTask(Return);

      _caseExporter = Locator.Current.GetService<ICaseExporter>();
      _caseRepository = Locator.Current.GetService<ICaseRepository>();
      _mapper = Locator.Current.GetService<IMapper>();

      Cases = _mapper.Map<ExportItem[]>(_caseRepository.GetAll());
    }

    public ReactiveCommand ReturnCommand {get;}
    private Task<Unit> Return()
    {
      HostScreen.Router.NavigateBack.Execute();
      return Task.FromResult(Unit.Default);
    }
    public ReactiveCommand OpenSaveFileDialogCommand {get;}
    private async Task OpenSaveFileDialog()
    {
      SaveFileDialog d = new SaveFileDialog();
      d.Filters.Add(new FileDialogFilter{ Name="Lincon Case", Extensions= { "lc"}});
      d.Filters.Add(new FileDialogFilter{ Name="All", Extensions= { "*"}});
      var result = await d.ShowAsync(new Window());
      if(result != null)
      {
        ExportPath = result;
      }      
    }

    public ReactiveCommand ExportCasesCommand {get;}
    private Task ExportCases()
    {
      var checkedItems = Cases.Where(x => x.IsChecked);
      var allCases = _caseRepository.GetAll();
      var casesToExport = allCases.Where(x => checkedItems.Any(y => y.ID == x.ID));
      _caseExporter.Export(casesToExport,ExportPath);
      return Task.FromResult(0);
    }
  }
}