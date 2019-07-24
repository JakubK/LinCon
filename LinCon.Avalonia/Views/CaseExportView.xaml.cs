using Avalonia;
using Avalonia.Markup.Xaml;
using LinCon.Avalonia.ViewModels;
using ReactiveUI;

namespace LinCon.Avalonia.Views
{
    public class CaseExportView : ReactiveUserControl<CaseExportViewModel>
    {
      public CaseExportView()
      {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
      }
    }
}