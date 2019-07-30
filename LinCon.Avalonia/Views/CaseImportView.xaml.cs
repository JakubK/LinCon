using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using LinCon.Avalonia.ViewModels;

namespace LinCon.Avalonia.Views
{
    public class CaseImportView : ReactiveUserControl<CaseImportViewModel>
    {
        private Border _DropState;
        private Path _Path;
        public CaseImportView()
        {
            this.InitializeComponent();
            
            AddHandler(DragDrop.DropEvent, Drop);
        }

        private void Drop(object sender, DragEventArgs e)
        {
            if (e.Data.Contains(DataFormats.FileNames))
            {
                // _DropState.Text = string.Join(Environment.NewLine, e.Data.GetFileNames());
                ViewModel.DropCommand.Execute(e);
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            _DropState = this.Find<Border>("DropState");
            _Path = this.Find<Path>("Path");
        }
    }
}