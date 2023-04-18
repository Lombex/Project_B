using System.Text.Json;

static class FlightInfoAccess
{
    static string path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @"DataSources/flightinfo.json"));

    public static List<FlightInfoModel> LoadAll()
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<FlightInfoModel>>(json)!;
    }


    public static void WriteAll(List<FlightInfoModel> accounts)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(accounts, options);
        File.WriteAllText(path, json);
    }


}