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

    public List<FlightInfoModel>? SortByFlightNumber() => _flightinfo.OrderBy(i => i.FlightNumber).ToList();
    public List<FlightInfoModel>? SortByAircraft() => _flightinfo.OrderBy(i => i.Aircraft).ToList();
    public List<FlightInfoModel>? SortByOrigin() => _flightinfo.OrderBy(i => i.Origin).ToList();
    public List<FlightInfoModel>? SortByDestination() => _flightinfo.OrderBy(i => i.Destination).ToList();
    public List<FlightInfoModel>? SortByDate() => _flightinfo.OrderBy(i => i.Date).ToList();
    public List<FlightInfoModel>? SortByFlightTime() => _flightinfo.OrderBy(i => i.FlightTime).ToList();
    public List<FlightInfoModel>? SortByDepartTime() => _flightinfo.OrderBy(i => i.DepartTime).ToList();
    public List<FlightInfoModel>? SortByArrivalTime() => _flightinfo.OrderBy(i => i.ArrivalTime).ToList();
    public List<FlightInfoModel>? SortByGate() => _flightinfo.OrderBy(i => i.Gate).ToList();





}