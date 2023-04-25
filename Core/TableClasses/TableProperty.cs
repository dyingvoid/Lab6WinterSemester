namespace Core.TableClasses;

public class TableProperty
{
    public string Name { get; set; }
    public string TypeName { get; set; }

    public bool IsNullOrEmpty()
    {
        return Name is null ||
               TypeName is null ||
               Name.Length == 0 ||
               TypeName.Length == 0;
    }
}