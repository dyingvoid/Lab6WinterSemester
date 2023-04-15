using Core.Managers;
using Core.Reflection;

namespace Core.TableClasses;

public class TableFactory
{
    public List<ReflectionTable> BuildTables(Dictionary<FileInfo, Dictionary<string, Type>> tableConfig)
    {
        var tables = new List<ReflectionTable>();

        var counter = 0;
        foreach (var (tableFile, tableMetadata) in tableConfig)
        {
            var builder = new ReflectionBuilder(tableFile.Name, tableMetadata);
            tables.Add(BuildTable(tableFile, tableMetadata, builder));
            counter++;
        }

        return tables;
    }

    private ReflectionTable BuildTable(FileInfo tableFile, 
        Dictionary<string, Type> tableMetadata, 
        ReflectionBuilder builder)
    {
        var preparedData = new List<object>();
        var tableData = tableFile.ReadFileData<object>();

        foreach (var elementArray in tableData)
        {
            preparedData.Add(builder.CreateInstance(elementArray.ToArray()));
        }

        return new ReflectionTable(tableFile, tableMetadata, preparedData, builder.BuildedType);
    }
}