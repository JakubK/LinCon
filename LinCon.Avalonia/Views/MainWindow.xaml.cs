using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using LinCon.Avalonia.ViewModels;

namespace LinCon.Avalonia.Views
{
    public class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}