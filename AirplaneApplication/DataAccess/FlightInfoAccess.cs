public class FlightInfoAccess : DataAccess<FlightInfoModel>
{
    public FlightInfoAccess()
    {
        path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/flightinfo.json"));
    }
}