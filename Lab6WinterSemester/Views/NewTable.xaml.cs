using System.Collections.Generic;
using System.Windows;
using Core.TableClasses;

namespace Lab6WinterSemester.Views;

public partial class NewTable : Window
{
    public NewTable(List<TableProperty> properties)
    {
        InitializeComponent();
        Properties = properties;
        DataContext = Properties;
    }
    
    public List<TableProperty> Properties { get; set; }
}