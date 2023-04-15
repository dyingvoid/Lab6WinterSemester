namespace Core.TableClasses;

public class ReflectionTable
{
    public ReflectionTable(FileInfo file, Dictionary<string, Type> metadata, List<object> data, Type type)
    {
        File = file;
        Metadata = metadata;
        ElementsType = type;
        Data = data;
    }

    public string Name => File.Name;
    public FileInfo File { get; }
    public Dictionary<string, Type> Metadata { get; }
    public Tuple<int, int> Shape => Tuple.Create(ElementsType.GetProperties().Length, Data.Count);
    public List<string> Names { get; }
    public Type ElementsType { get; }
    public List<object> Data { get; set; }
}