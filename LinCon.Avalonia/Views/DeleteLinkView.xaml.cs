using Avalonia;
using Avalonia.Markup.Xaml;
using LinCon.Avalonia.ViewModels;
using ReactiveUI;

namespace LinCon.Avalonia.Views
{
    public class DeleteLinkView : ReactiveUserControl<DeleteLinkViewModel>
    {
      public DeleteLinkView()
      {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
      }
    }
}