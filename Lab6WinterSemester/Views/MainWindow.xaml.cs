using System.Windows;
using Core.Reflection;
using Core.TableClasses;
using Lab6WinterSemester.Models;
using Lab6WinterSemester.ViewModels;
using Table = Core.TableClasses.Table;

namespace Lab6WinterSemester.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        MainModel mainModel = new MainModel();
        DataContext = new MainWindowViewModel(mainModel);
        BtnDeleteTable.IsEnabled = false;
    }
    
    private void UpdateDataGrid(object sender, RoutedEventArgs e)
    {
        if (Explorer.SelectedItem is null)
            return;
        
        if (Explorer.SelectedItem is Table)
        {
            var table = ((Table)Explorer.SelectedItem);
            if (table.Data.Count == 0)
            {
                var description = ReflectionBuilder.GetDescription(table);
                var builder = new ReflectionBuilder("System." + table.ElementsType.Name, description);
                table.Data.Add(builder.CreateEmptyInstance());
            }

            BtnDeleteTable.IsEnabled = true;
            Data.ItemsSource = table.Data;
            Description.ItemsSource = ((Table)Explorer.SelectedItem).Properties;
            
        }
        else
        {
            Data.ItemsSource = ((DataBase)Explorer.SelectedItem).Tables;
            BtnDeleteTable.IsEnabled = false;
        }
    }

    private void BtnDeleteTable_OnClick(object sender, RoutedEventArgs e)
    {
        var dataBases = ((MainWindowViewModel)DataContext).DataBases;
        foreach (var database in dataBases)
        {
            if (database.Tables.Contains(((Table)Explorer.SelectedItem)))
                database.Tables.Remove(((Table)Explorer.SelectedItem));
        }
    }
}