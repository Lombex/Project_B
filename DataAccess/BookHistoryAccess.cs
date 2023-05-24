using System.Text.Json;

static class BookHistoryAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/bookhistory.json"));

    public static List<BookHistoryModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<BookHistoryModel>>(json)!;
    }


    public static void WriteAll(List<BookHistoryModel> booking)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(booking, options);
        File.WriteAllText(path, json);
    }
}