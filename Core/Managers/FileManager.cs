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

    public static void Save(this ReflectionDataBase dataBase)
    {
        foreach (var table in dataBase.Tables)
        {
            SaveTable(table);
        }
    }

    private static void SaveTable(ReflectionTable table)
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
}