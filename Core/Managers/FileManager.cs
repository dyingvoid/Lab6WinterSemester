using System.Text.Json;
using Core.Reflection;
using Core.TableClasses;

namespace Core.Managers;

public static class FileManager
{
    public static List<List<T>> ReadFileData<T>(this FileInfo file)
    {
        var fileData = new List<List<T>>();
        
        if (file.Exists)
        {
            var fileRows = File.ReadAllLines(file.FullName);
            foreach (var str in fileRows)
            {
                var list = new List<T>();
                var splitString = str.Split(",");
                
                foreach (var item in splitString)
                {
                    list.Add((T)Convert.ChangeType(item, typeof(T)));
                }
                
                fileData.Add(list);
            }
        }

        return fileData;
    }

    public static void Save(this DataBase dataBase)
    {
        foreach (var table in dataBase.Tables)
        {
            SaveTable(table);
        }
        SaveConfig(dataBase);
    }

    private static void SaveTable(Table table)
    {
        var data = new List<string>();
        using var writer = new StreamWriter(table.File.FullName);
        
        foreach (var element in table.Data)
        {
            var properties = from property in ReflectionBuilder.GetProperties(element)
                select property.ToString();

            writer.WriteLine(String.Join(",", properties));
        }
    }

    private static void SaveConfig(DataBase database)
    {
        var description = MakeStringConfig(database);
        var strConfig = JsonSerializer.Serialize(description);

        using var writer = new StreamWriter(database.File.DataBaseFile.FullName);
        writer.WriteLine(strConfig);
    }

    private static Dictionary<string, Dictionary<string, string>> MakeStringConfig(DataBase database)
    {
        var description = new Dictionary<string, Dictionary<string, string>>();

        foreach (var table in database.Tables)
        {
            var strMetaData = new Dictionary<string, string>();

            foreach (var (name, type) in table.Metadata)
            {
                strMetaData.Add(name, type.FullName);
            }
            description.Add(table.File.FullName, strMetaData);
        }

        return description;
    }
}