namespace Core.TableClasses;

public class ReflectionDataBase
{
    public ReflectionDataBase(SchemaFile file, List<ReflectionTable> tables)
    {
        File = file;
        Tables = tables;
    }
    public SchemaFile File { get; }
    public List<ReflectionTable> Tables { get; }
}