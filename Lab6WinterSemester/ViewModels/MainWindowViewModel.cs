using System.Collections.ObjectModel;
using System.Windows.Input;
using Core.TableClasses;
using Lab6WinterSemester.Models;

namespace Lab6WinterSemester.ViewModels;

public class MainWindowViewModel
{
    private readonly MainModel _model;
    private ICommand _addDataBaseCommand;
    private ICommand _saveCommand;

    public ICommand AddDataBaseCommand
    {
        get
        {
            if (_addDataBaseCommand == null)
            {
                _addDataBaseCommand = new RelayCommand(
                    param => _model.AddDataBase(),
                    param => true
                    );
            }
            return _addDataBaseCommand;
        }
    }

    public ICommand SaveCommand
    {
        get
        {
            if (_saveCommand == null)
            {
                _saveCommand= new RelayCommand(
                    param => _model.Save(),
                    param => true
                );
            }
            return _saveCommand;
        }
    }

    public MainWindowViewModel(MainModel model)
    {
        _model = model;
        DataBases = _model.DataBases;
    }

    public ObservableCollection<DataBase> DataBases { get; set; }
}