using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

public class FlightInfoLogic
{
    // private field (list) with json info


    private List<FlightInfoModel> _flightinfo;
    public FlightInfoLogic()
    {
        FlightInfoAccess flightinfoAccess = new FlightInfoAccess();
        _flightinfo = flightinfoAccess.LoadAll();
    }

    public List<FlightInfoModel>? SortByFlightNumber(bool ascending = true)
    {
        var orderByResult = from s in _flightinfo
                            orderby s.FlightNumber
                            select s;

        var orderByDescendingResult = from s in _flightinfo
                                      orderby s.FlightNumber descending
                                      select s;
        var sortedList = ascending
            ? orderByResult.ToList()
            : orderByDescendingResult.ToList();

        return sortedList;
    }

    public List<FlightInfoModel>? SortByAircraft(bool ascending = true)
    {
        var orderByResult = from s in _flightinfo
                            orderby s.Aircraft
                            select s;

        var orderByDescendingResult = from s in _flightinfo
                                      orderby s.Aircraft descending
                                      select s;
        var sortedList = ascending
            ? orderByResult.ToList()
            : orderByDescendingResult.ToList();

        return sortedList;
    }

    public List<FlightInfoModel>? SortByOrigin(bool ascending = true)
    {
        var orderByResult = from s in _flightinfo
                            orderby s.Origin
                            select s;

        var orderByDescendingResult = from s in _flightinfo
                                      orderby s.Origin descending
                                      select s;
        var sortedList = ascending
            ? orderByResult.ToList()
            : orderByDescendingResult.ToList();

        return sortedList;
    }

    public List<FlightInfoModel>? SortByDestination(bool ascending = true)
    {
        var orderByResult = from s in _flightinfo
                            orderby s.Destination
                            select s;

        var orderByDescendingResult = from s in _flightinfo
                                      orderby s.Destination descending
                                      select s;
        var sortedList = ascending
            ? orderByResult.ToList()
            : orderByDescendingResult.ToList();

        return sortedList;
    }

    public List<FlightInfoModel>? SortByDate(bool ascending = true)
    {
        var orderByResult = from s in _flightinfo
                            orderby s.Date
                            select s;

        var orderByDescendingResult = from s in _flightinfo
                                      orderby s.Date descending
                                      select s;
        var sortedList = ascending
            ? orderByResult.ToList()
            : orderByDescendingResult.ToList();

        return sortedList;
    }

    public List<FlightInfoModel>? SortByFlightTime(bool ascending = true)
    {
        var orderByResult = from s in _flightinfo
                            orderby s.FlightTime
                            select s;

        var orderByDescendingResult = from s in _flightinfo
                                      orderby s.FlightTime descending
                                      select s;
        var sortedList = ascending
            ? orderByResult.ToList()
            : orderByDescendingResult.ToList();

        return sortedList;
    }

    public List<FlightInfoModel>? SortByDepartTime(bool ascending = true)
    {
        var orderByResult = from s in _flightinfo
                            orderby s.DepartTime
                            select s;

        var orderByDescendingResult = from s in _flightinfo
                                      orderby s.DepartTime descending
                                      select s;
        var sortedList = ascending
            ? orderByResult.ToList()
            : orderByDescendingResult.ToList();

        return sortedList;
    }

    public List<FlightInfoModel>? SortByArrivalTime(bool ascending = true)
    {
        var orderByResult = from s in _flightinfo
                            orderby s.ArrivalTime
                            select s;

        var orderByDescendingResult = from s in _flightinfo
                                      orderby s.ArrivalTime descending
                                      select s;
        var sortedList = ascending
            ? orderByResult.ToList()
            : orderByDescendingResult.ToList();

        return sortedList;
    }

    public List<FlightInfoModel>? SortByGate(bool ascending = true)
    {
        var orderByResult = from s in _flightinfo
                            orderby s.Gate
                            select s;

        var orderByDescendingResult = from s in _flightinfo
                                      orderby s.Gate descending
                                      select s;
        var sortedList = ascending
            ? orderByResult.ToList()
            : orderByDescendingResult.ToList();

        return sortedList;
    }


    public List<FlightInfoModel> SearchByName(string possible_destination)
    {
        List<FlightInfoModel> possible_flights = new List<FlightInfoModel>();

        foreach (FlightInfoModel item in _flightinfo)
        {
            if (item.Destination == possible_destination)
            {
                possible_flights.Add(item);
            }
        }

        return possible_flights;
    }





}