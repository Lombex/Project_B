using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

public class FlightInfoLogic
{
    // private field (list) with json info
    private List<FlightInfoModel> _flightinfo;

    static public FlightInfoModel? current_flight { get; private set; }

    public FlightInfoLogic()
    {
        _flightinfo = FlightInfoAccess.LoadAll();
    }

    public FlightInfoModel? GetByFlightNumber(string searchTerm) => _flightinfo.Find(i => i.FlightNumber == searchTerm);
    public FlightInfoModel? GetByAircraft(string searchTerm) => _flightinfo.Find(i => i.Aircraft == searchTerm);
    public FlightInfoModel? GetByOrigin(string searchTerm) => _flightinfo.Find(i => i.Origin == searchTerm);
    public FlightInfoModel? GetByDestination(string searchTerm) => _flightinfo.Find(i => i.Destination == searchTerm);
    public FlightInfoModel? GetByDate(string searchTerm) => _flightinfo.Find(i => i.Date == searchTerm);
    public FlightInfoModel? GetByFlightTime(double searchTerm) => _flightinfo.Find(i => i.FlightTime == searchTerm);
    public FlightInfoModel? GetByDepartTime(string searchTerm) => _flightinfo.Find(i => i.DepartTime == searchTerm);
    public FlightInfoModel? GetByArrivalTime(string searchTerm) => _flightinfo.Find(i => i.ArrivalTime == searchTerm);
    public FlightInfoModel? GetByGate(string searchTerm) => _flightinfo.Find(i => i.Gate == searchTerm);





}