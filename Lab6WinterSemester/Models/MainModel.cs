﻿using System.Collections.ObjectModel;
using System.IO;
using Core.TableClasses;
using Microsoft.Win32;

namespace Lab6WinterSemester.Models;

public class MainModel : IMainModel
{
    public ObservableCollection<DataBase> DataBases { get; set; }

    public MainModel()
    {
        DataBases = new ObservableCollection<DataBase>();
    }

    public bool AddDataBase()
    {
        var file = OpenDataBaseFile();
        if (file == null) 
            return false;

        DataBases.Add(CreateDataBase(file));
        return true;
    }

    private FileInfo? OpenDataBaseFile()
    {
        var fileDialog = new OpenFileDialog();
        if (fileDialog.ShowDialog() == true)
        { 
            return new FileInfo(fileDialog.FileName);
        }

        return null;
    }
    
    private DataBase CreateDataBase(FileInfo dataBaseSchemaFile)
    {
        DataBaseSimpleFactory factory = new DataBaseSimpleFactory();
        return factory.CreateDataBase(dataBaseSchemaFile);
    }
}