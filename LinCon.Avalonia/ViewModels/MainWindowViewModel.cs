using ReactiveUI;

namespace LinCon.Avalonia.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        
        public RoutingState Router { get; } = new RoutingState();     

        public MainWindowViewModel()
        {
            Router.Navigate.Execute(new MenuViewModel(this));
        }
    }
}
