using Avalonia;
using Avalonia.Markup.Xaml;
using LinCon.Avalonia.ViewModels;
using ReactiveUI;

namespace LinCon.Avalonia.Views
{
    public class AddCaseView : ReactiveUserControl<AddCaseViewModel>
    {
      public AddCaseView()
      {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
      }
    }
}