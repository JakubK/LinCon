using Avalonia;
using Avalonia.Markup.Xaml;
using LinCon.Avalonia.ViewModels;
using ReactiveUI;

namespace LinCon.Avalonia.Views
{
    public class MenuView : ReactiveUserControl<MenuViewModel>
    {
      public MenuView()
      {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
      }
    }
}