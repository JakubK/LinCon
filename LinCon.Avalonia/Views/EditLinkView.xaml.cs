using Avalonia;
using Avalonia.Markup.Xaml;
using LinCon.Avalonia.ViewModels;
using ReactiveUI;

namespace LinCon.Avalonia.Views
{
    public class EditLinkView : ReactiveUserControl<EditLinkViewModel>
    {
      public EditLinkView()
      {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
      }
    }
}