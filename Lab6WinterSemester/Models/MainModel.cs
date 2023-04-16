using System.Collections.ObjectModel;
using System.IO;
using Core.Loggers;
using Core.Managers;
using Core.TableClasses;
using Core.Testers;
using Microsoft.Win32;

namespace Lab6WinterSemester.Models;

public class MainModel
{
    public MainModel()
    {
        DataBases = new ObservableCollection<DataBase>();
        DbFactory = new DataBaseFactory(new TableFactory());
        Tester = new FileTester(Logger.GetInstance());
    }
    
    public ObservableCollection<DataBase> DataBases { get; set; }
    public DataBaseFactory DbFactory { get; set; }
    public FileTester Tester { get; set; }

    public bool AddDataBase()
    {
        var file = OpenDataBaseFile();
        if (file == null) 
            return false;

        var database = CreateDataBase(file);
        
        if (database is null)
            return false;
        
        DataBases.Add(database);
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
    
    private DataBase? CreateDataBase(FileInfo dataBaseFile)
    {
        Tester.Test(dataBaseFile, out var schemaFile);
        
        if(schemaFile is not null)
            return DbFactory.CreateInstance(schemaFile);

        return null;
    }

    public void Save()
    {
        foreach (var database in DataBases)
        {
            FileManager.Save(database);
        }
    }
}