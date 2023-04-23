namespace Core.TableClasses;

public class Table
{
    public Table(FileInfo file, Dictionary<string, Type> metadata, List<object> data, Type type)
    {
        File = file;
        Metadata = metadata;
        ElementsType = type;
        Data = data;
    }

    public string Name
    {
        get => File.Name;
        set
        {
            var destination = File.FullName.Substring(0, File.FullName.IndexOf(File.Name)) + value;
            System.IO.File.Move(File.FullName, destination);
            File = new FileInfo(destination);
        }
    }

    public FileInfo File { get; private set; }
    public Dictionary<string, Type> Metadata { get; }
    public Tuple<int, int> Shape => Tuple.Create(ElementsType.GetProperties().Length, Data.Count);
    public List<string> Names { get; }
    public Type ElementsType { get; }
    public List<object> Data { get; set; }
}