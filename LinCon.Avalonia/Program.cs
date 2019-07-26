using System;
using Avalonia;
using Avalonia.Logging.Serilog;
using LinCon.Avalonia.ViewModels;
using LinCon.Avalonia.Views;
using LinCon.Core.Services;
using LinCon.Core.Services.Abstract;
using ReactiveUI;
using Splat;

namespace LinCon.Avalonia
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
            => BuildAvaloniaApp().Start(AppMain, args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp() =>
             AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToDebug()
            .UseReactiveUI();

        // Your application's entry point. Here you can initialize your MVVM framework, DI
        // container, etc.
        private static void AppMain(Application app, string[] args)
        {
            var window = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
            
            Locator.CurrentMutable.Register(() => new CaseImporter(), typeof(ICaseImporter));
            Locator.CurrentMutable.Register(() => new PathProvider(), typeof(IPathProvider));
            Locator.CurrentMutable.Register(() => new CaseRepository((Locator.CurrentMutable.GetService<IPathProvider>())), typeof(ICaseRepository));
            Locator.CurrentMutable.Register(() => new CaseProcessor(), typeof(ICaseProcessor));
            Locator.CurrentMutable.Register(() => new CaseExporter(), typeof(ICaseExporter));

            Locator.CurrentMutable.Register(() => new MenuView(), typeof(IViewFor<MenuViewModel>));
            Locator.CurrentMutable.Register(() => new CaseExportView(), typeof(IViewFor<CaseExportViewModel>));
            Locator.CurrentMutable.Register(() => new CaseImportView(), typeof(IViewFor<CaseImportViewModel>));
            Locator.CurrentMutable.Register(() => new CaseExplorerView(), typeof(IViewFor<CaseExplorerViewModel>));

            app.Run(window);
        }
    }
}
