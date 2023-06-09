namespace Terminal;

public static class ConsoleTools
{
    public static void ShowError(this string error)
    {
        ModelError(error);
    }

    public static void ShowListWithIndex<T>(
        this List<T> list,
        string view)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"**** {view} ****");
        if (list.Any())
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"Index {i}: {list[i]}");
            }    
        }
        else
        {
            throw new Exception($"{view} not found");
        }
        

        Console.WriteLine($"**** {view} ****");
        Console.ResetColor();
    }
    
    public static void CheckIndexValid<T>(
        this List<T> list,
        string userIndex)
    {
        if (!int.TryParse(userIndex, out var index) ||
            index < 0 ||
            index >= list.Count)
        {
            throw new Exception("Index Not Valid");
        }
    }
    public static void ShowSuccessful(this string value)
    {
        ModelSuccessful(value);
    }
    
    public static string GetStringValue(string value)
    {
        ModelSuccessful(value);
        var input = Console.ReadLine()!;
        return input;
    }

    private static void ModelError(string value)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        var result = "**** Error ***\n" +
                     value +
                     "\n" +
                     "**** Error ****";
        Console.WriteLine(result);
        Console.ResetColor();
    }
    
    public static void ModelPrimary(string value)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        var result = "**** (-) ***\n" +
                     value +
                     "\n" +
                     "**** (+) ****";
        Console.WriteLine(result);
        Console.ResetColor();
    }

    private static void ModelSuccessful(string value)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        var result = "----\n" +
                     value +
                     "\n" +
                     "----";
        Console.WriteLine(result);
        Console.ResetColor();
    }

    public static void ModelWarning(string value)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        var result = "**** ---- ****\n" +
                     value +
                     "\n" +
                     "**** ---- ****";
        Console.WriteLine(result);
        Console.ResetColor();
    }
}