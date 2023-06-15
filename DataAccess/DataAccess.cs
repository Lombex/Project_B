using System.Text.Json;

public abstract class DataAccess<T>
{
    protected string? path;

    public List<T> LoadAll()
    {
        string json = File.ReadAllText(path!);
        return JsonSerializer.Deserialize<List<T>>(json)!;
    }

    public void WriteAll(List<T> data)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(data, options);
        File.WriteAllText(path!, json);
    }
}