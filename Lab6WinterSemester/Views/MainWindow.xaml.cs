using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Core.TableClasses;
using Lab6WinterSemester.Models;
using Lab6WinterSemester.ViewModels;

namespace Lab6WinterSemester.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        MainModel mainModel = new MainModel();
        DataContext = new MainWindowViewModel(mainModel);
    }
    
    private void UpdateDataGrid(object sender, RoutedEventArgs e)
    {
        if(Explorer.SelectedItem is Table)
            Data.ItemsSource = ((Table)Explorer.SelectedItem).Data;
        else
            Data.ItemsSource = ((DataBase)Explorer.SelectedItem).Tables;
        
    }

    private void Data_OnCellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
    {
        if (e.EditAction == DataGridEditAction.Commit)
        {
            var column = e.Column as DataGridBoundColumn;
            if (column != null)
            {
                var bindingPath = (column.Binding as Binding).Path.Path;
                
                int rowIndex = e.Row.GetIndex();
                var el = e.EditingElement as TextBox;
                // rowIndex has the row index
                // bindingPath has the column's binding
                // el.Text has the new, user-entered value
                
            }
        }
    }
}