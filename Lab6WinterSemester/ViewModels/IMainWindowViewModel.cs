using System.Collections.ObjectModel;
using System.Windows.Input;
using Core.TableClasses;

namespace Lab6WinterSemester.ViewModels;

public interface IMainWindowViewModel
{
    ObservableCollection<DataBase> DataBases { get; set; }
    ICommand AddDataBaseCommand { get; }
}