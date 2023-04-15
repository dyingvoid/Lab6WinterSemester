namespace Core.TableClasses;

public static class TableExtensions
{
    public static bool IsEmptyOrWhiteSpace(this object? value)
    {
        var str = value?.ToString();
        return str is "" or " ";
    }

    public static Dictionary<int, List<object?>> ToDictionary(this List<List<object?>> list)
    {
        var dict = new Dictionary<int, List<object?>>();
        
        for (var i = 0; i < list.Count; ++i)
        {
            if(list[i].Count == 0)
                continue;

            var column = new List<object?>();
            for (var j = 0; j < list[i].Count; ++j)
            {
                column.Add(list[j][i]);
            }
            
            dict.Add(i, column);
        }

        return dict;
    }
}