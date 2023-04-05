namespace Core.TableClasses;

public interface IDataBase
{
    FileInfo SchemaFile { get; set; }
    public List<Table> Tables { get; set; }
    Dictionary<FileInfo, Dictionary<string, Type>> Config { get; set; }
}