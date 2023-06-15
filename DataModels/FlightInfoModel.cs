using System.Text.Json.Serialization;

public class FlightInfoModel
{
    [JsonPropertyName("Flight-ID")]
    public int FlightID { get; set; }

    [JsonPropertyName("Flight-number")]
    public string FlightNumber { get; set; }

    [JsonPropertyName("Aircraft")]
    public string Aircraft { get; set; }

    [JsonPropertyName("Origin")]
    public string Origin { get; set; }

    [JsonPropertyName("Destination")]
    public string Destination { get; set; }

    [JsonPropertyName("Date")]
    public string Date { get; set; }

    [JsonPropertyName("FlightTime")]
    public double FlightTime { get; set; }

    [JsonPropertyName("DepartTime")]
    public string DepartTime { get; set; }

    [JsonPropertyName("ArrivalTime")]
    public string ArrivalTime { get; set; }

    [JsonPropertyName("Gate")]
    public string Gate { get; set; }

    [JsonPropertyName("SeatsTaken")]
    public List<string> SeatsTaken { get; set; }

    [JsonPropertyName("Price")]
    public int Price { get; set; }

    [JsonPropertyName("WindowMultuplier")]
    public double WindowMultuplier { get; set; }
    [JsonPropertyName("FirstClassMutiplier")]
    public double FirstClassMutiplier { get; set; }

    public FlightInfoModel(int flightID, string flightnumber, string aircraft, string origin, string destination, string date, double flighttime, string departtime, string arrivaltime, int price, string gate = "A1", double windowMultuplier = 1.2, double firstClassMutiplier = 2)
    {
        FlightID = flightID;
        FlightNumber = flightnumber;
        Aircraft = aircraft;
        Origin = origin;
        Destination = destination;
        Date = date;
        FlightTime = flighttime;
        DepartTime = departtime;
        ArrivalTime = arrivaltime;
        Gate = gate;
        SeatsTaken = new List<string>();
        Price = price;
        WindowMultuplier = windowMultuplier;
        FirstClassMutiplier = firstClassMutiplier;
    }
}