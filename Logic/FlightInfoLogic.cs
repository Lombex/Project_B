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

    



}