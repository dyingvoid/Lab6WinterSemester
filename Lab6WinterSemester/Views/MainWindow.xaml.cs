using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Controls;
using Core.TableClasses;
using Lab6WinterSemester.Models;
using Lab6WinterSemester.ViewModels;

namespace Lab6WinterSemester.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainModel mainModel = new MainModel();
            DataContext = new MainWindowViewModel(mainModel);
        }

        private string num { get; set; }
        
        public static readonly DependencyProperty DataViewProperty = DependencyProperty.Register(
            nameof(DataView),
            typeof(DataView),
            typeof(MainWindow)
            );
        
        public DataView DataView
        {
            get => (DataView)GetValue(DataViewProperty);
            set => SetValue(DataViewProperty, value);
        }

        private void UpdateDataGrid(object sender, RoutedEventArgs e)
        {
            try
            {
                Data.ItemsSource = ((ReflectionTable)Explorer.SelectedItem).Data;
            }
            catch
            {
            }
        }
    }
}