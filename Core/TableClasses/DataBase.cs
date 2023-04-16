namespace Core.TableClasses;

public class DataBase
{
    public DataBase(SchemaFile file, List<Table> tables)
    {
        File = file;
        Tables = tables;
    }
    public SchemaFile File { get; }
    public List<Table> Tables { get; }
}