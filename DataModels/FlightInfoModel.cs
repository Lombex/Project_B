using System.Text.Json.Serialization;

public class FlightInfoModel
{
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

    public FlightInfoModel(string flightnumber, string aircraft, string origin, string destination, string date, double flighttime, string departtime, string arrivaltime, string gate = "A1")
    {
        FlightNumber = flightnumber;
        Aircraft = aircraft;
        Origin = origin;
        Destination = destination;
        Date = date;
        FlightTime = flighttime;
        DepartTime = departtime;
        ArrivalTime = arrivaltime;
        Gate = gate;
    }
}