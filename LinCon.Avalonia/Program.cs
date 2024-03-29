﻿using System;
using System.IO;
using AutoMapper;
using Avalonia;
using Avalonia.Logging.Serilog;
using LinCon.Avalonia.Models;
using LinCon.Avalonia.ViewModels;
using LinCon.Avalonia.Views;
using LinCon.Core.Models;
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
        {
            bool runApp = true;

            CaseImporter importer = new CaseImporter();
            CaseProcessor processor = new CaseProcessor();
            foreach(string arg in args)
            {
                if(File.Exists(arg))
                {
                    runApp = false;
                    
                    var cases = importer.Import(arg);
                    foreach(var caseItem in cases)
                    {
                        processor.ProcessCase(caseItem);
                    }
                }
            }

            if(!runApp)
                return;

            BuildAvaloniaApp().Start(AppMain, args);
        }

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
        
            var config = new MapperConfiguration(cfg =>
            {
               cfg.CreateMap<Case, ExportItem>(); 
               cfg.CreateMap<Case, CaseItem>(); 
               cfg.CreateMap<ExportItem, CaseItem>(); 
               cfg.CreateMap<Link, DeleteLinkItem>(); 
               cfg.CreateMap<Link, LinkItem>(); 

            });
            var mapper = config.CreateMapper();

            Locator.CurrentMutable.Register(() => new CaseImporter(), typeof(ICaseImporter));
            Locator.CurrentMutable.Register(() => new PathProvider(), typeof(IPathProvider));
            Locator.CurrentMutable.RegisterLazySingleton(() => new CaseRepository((Locator.CurrentMutable.GetService<IPathProvider>())), typeof(ICaseRepository));
            Locator.CurrentMutable.RegisterLazySingleton(() => mapper,typeof(IMapper));
            Locator.CurrentMutable.Register(() => new CaseProcessor(), typeof(ICaseProcessor));
            Locator.CurrentMutable.Register(() => new CaseExporter(), typeof(ICaseExporter));

            Locator.CurrentMutable.Register(() => new MenuView(), typeof(IViewFor<MenuViewModel>));
            Locator.CurrentMutable.Register(() => new CaseExportView(), typeof(IViewFor<CaseExportViewModel>));
            Locator.CurrentMutable.Register(() => new CaseImportView(), typeof(IViewFor<CaseImportViewModel>));
            Locator.CurrentMutable.Register(() => new CaseExplorerView(), typeof(IViewFor<CaseExplorerViewModel>));
            Locator.CurrentMutable.Register(() => new CaseView(), typeof(IViewFor<CaseViewModel>));
            Locator.CurrentMutable.Register(() => new AddLinkView(), typeof(IViewFor<AddLinkViewModel>));
            Locator.CurrentMutable.Register(() => new DeleteLinkView(), typeof(IViewFor<DeleteLinkViewModel>));
            Locator.CurrentMutable.Register(() => new DeleteManyLinksView(), typeof(IViewFor<DeleteManyLinksViewModel>));
            Locator.CurrentMutable.Register(() => new DeleteManyCasesView(), typeof(IViewFor<DeleteManyCasesViewModel>));
            Locator.CurrentMutable.Register(() => new EditLinkView(), typeof(IViewFor<EditLinkViewModel>));

            Locator.CurrentMutable.Register(() => new DeleteCaseView(), typeof(IViewFor<DeleteCaseViewModel>));
            Locator.CurrentMutable.Register(() => new AddCaseView(), typeof(IViewFor<AddCaseViewModel>));
            Locator.CurrentMutable.Register(() => new EditCaseView(), typeof(IViewFor<EditCaseViewModel>));

            app.Run(window);
        }
    }
}
