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
}