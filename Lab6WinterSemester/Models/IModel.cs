using System.Collections.ObjectModel;
using Core.TableClasses;


namespace Lab6WinterSemester.Models;

public interface IMainModel
{
    public ObservableCollection<DataBase> DataBases { get; set; }
    public bool AddDataBase();
}