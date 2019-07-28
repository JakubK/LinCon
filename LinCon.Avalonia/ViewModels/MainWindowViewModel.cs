using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ReactiveUI;

namespace LinCon.Avalonia.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        
        public RoutingState Router { get; } = new RoutingState();     
        public ReactiveCommand<Unit, Unit> GoBack => Router.NavigateBack;

        public MainWindowViewModel()
        {
            Router.Navigate.Execute(new MenuViewModel(this));
        }
    }
}
