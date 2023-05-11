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

    public List<FlightInfoModel>? SortByFlightNumber(bool ascending = true)
    {
        var sortedList = ascending
            ? _flightinfo.OrderBy(i => i.FlightNumber)
            : _flightinfo.OrderByDescending(i => i.FlightNumber);

        return sortedList.ToList();
    }

    public List<FlightInfoModel>? SortByAircraft(bool ascending = true)
    {
        var sortedList = ascending
            ? _flightinfo.OrderBy(i => i.Aircraft)
            : _flightinfo.OrderByDescending(i => i.Aircraft);

        return sortedList.ToList();
    }

    public List<FlightInfoModel>? SortByOrigin(bool ascending = true)
    {
        var sortedList = ascending
            ? _flightinfo.OrderBy(i => i.Origin)
            : _flightinfo.OrderByDescending(i => i.Origin);

        return sortedList.ToList();
    }

    public List<FlightInfoModel>? SortByDestination(bool ascending = true)
    {
        var sortedList = ascending
            ? _flightinfo.OrderBy(i => i.Destination)
            : _flightinfo.OrderByDescending(i => i.Destination);

        return sortedList.ToList();
    }

    public List<FlightInfoModel>? SortByDate(bool ascending = true)
    {
        var sortedList = ascending
            ? _flightinfo.OrderBy(i => i.Date)
            : _flightinfo.OrderByDescending(i => i.Date);

        return sortedList.ToList();
    }

    public List<FlightInfoModel>? SortByFlightTime(bool ascending = true)
    {
        var sortedList = ascending
            ? _flightinfo.OrderBy(i => i.FlightTime)
            : _flightinfo.OrderByDescending(i => i.FlightTime);

        return sortedList.ToList();
    }

    public List<FlightInfoModel>? SortByDepartTime(bool ascending = true)
    {
        var sortedList = ascending
            ? _flightinfo.OrderBy(i => i.DepartTime)
            : _flightinfo.OrderByDescending(i => i.DepartTime);

        return sortedList.ToList();
    }

    public List<FlightInfoModel>? SortByArrivalTime(bool ascending = true)
    {
        var sortedList = ascending
            ? _flightinfo.OrderBy(i => i.ArrivalTime)
            : _flightinfo.OrderByDescending(i => i.ArrivalTime);

        return sortedList.ToList();
    }

    public List<FlightInfoModel>? SortByGate(bool ascending = true)
    {
        var sortedList = ascending
            ? _flightinfo.OrderBy(i => i.Gate)
            : _flightinfo.OrderByDescending(i => i.Gate);

        return sortedList.ToList();
    }





}