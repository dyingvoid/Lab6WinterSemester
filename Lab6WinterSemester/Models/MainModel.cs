using System.Collections.ObjectModel;
using System.IO;
using Core.Loggers;
using Core.TableClasses;
using Core.Testers;
using Microsoft.Win32;

namespace Lab6WinterSemester.Models;

public class MainModel
{
    public ObservableCollection<ReflectionDataBase> DataBases { get; set; }

    public MainModel()
    {
        DataBases = new ObservableCollection<ReflectionDataBase>();
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
    
    private ReflectionDataBase CreateDataBase(FileInfo dataBaseFile)
    {
        var tableFactory = new TableFactory();
        var databaseFactory = new DataBaseFactory(tableFactory);
        var fileTester = new FileTester(Logger.GetInstance());

        fileTester.Test(dataBaseFile, out var schemaFile);
        return databaseFactory.CreateInstance(schemaFile);
    }
}