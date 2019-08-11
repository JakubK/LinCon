using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using LinCon.Avalonia.ViewModels;

namespace LinCon.Avalonia.Views
{
    public class CaseImportView : ReactiveUserControl<CaseImportViewModel>
    {
        private Border _DropState;
        public CaseImportView()
        {
            this.InitializeComponent();
            
            AddHandler(DragDrop.DropEvent, Drop);
            AddHandler(DragDrop.DragOverEvent, DragOver);
        }

        private void DragOver(object sender, DragEventArgs e)
        {
            // Only allow Copy or Link as Drop Operations.
            e.DragEffects = e.DragEffects & (DragDropEffects.Copy | DragDropEffects.Link);
            // Only allow if the dragged data contains filenames.
            if (!e.Data.Contains(DataFormats.FileNames))
                e.DragEffects = DragDropEffects.None;
        }

        private void Drop(object sender, DragEventArgs e)
        {
            if (e.Data.Contains(DataFormats.FileNames))
                ViewModel.DropCommand.Execute(e);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            _DropState = this.Find<Border>("DropState");
        }
    }
}