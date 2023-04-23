using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Core.Reflection;
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
        UpdateDescription();
    }
    
    private void UpdateDataGrid(object sender, RoutedEventArgs e)
    {
        if (Explorer.SelectedItem is Table)
        {
            Data.ItemsSource = ((Table)Explorer.SelectedItem).Data;
            Description.ItemsSource = ((Table)Explorer.SelectedItem).ElementsType.GetProperties();
            
        }
        else
            Data.ItemsSource = ((DataBase)Explorer.SelectedItem).Tables;
    }

    private void UpdateDescription()
    {
        var nameColumn = new DataGridTextColumn() { Header = "Name", Binding = new Binding("Name") };
        var typeColumn = new DataGridTextColumn() { Header="Type", Binding = new Binding("PropertyType") };
        Description.Columns.Add(nameColumn);
        Description.Columns.Add(typeColumn);
    }
}