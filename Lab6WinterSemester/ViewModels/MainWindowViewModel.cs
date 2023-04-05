﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using Core.TableClasses;
using Lab6WinterSemester.Models;

namespace Lab6WinterSemester.ViewModels;

public class MainWindowViewModel : IMainWindowViewModel
{
    private readonly IMainModel _model;
    private ICommand _addDataBaseCommand;

    public ICommand AddDataBaseCommand
    {
        get
        {
            if (_addDataBaseCommand == null)
            {
                _addDataBaseCommand = new RelayCommand(
                    param => AddDataBase(),
                    param => true
                    );
            }
            return _addDataBaseCommand;
        }
    }

    public MainWindowViewModel(IMainModel model)
    {
        _model = model;
        DataBases = _model.DataBases;
    }

    public ObservableCollection<DataBase> DataBases { get; set; }

    private void AddDataBase()
    {
        _model.AddDataBase();
    }
}