using ConsoleTables;
public class ViewFlights
{
    private static List<FlightInfoModel>? _flights;

    public static ConsoleTableOptions options = new ConsoleTableOptions
    {
        EnableCount = false
    };

    public static void Menu()
    {
        if (_flights == null)
        {
            _flights = FlightInfoAccess.LoadAll();
        }
        FlightInfoLogic Fil = new FlightInfoLogic();
        FlightSchedule();
    }

    public static void SortingMenu()
    {

        List<string> main_account_choices = new List<string>() { " Enter 1 to sort on Flightnumber", " Enter 2 to sort on Aircraft", " Enter 3 to sort on Origin",
        " Enter 4 to sort on Destination", " Enter 5 to sort on Date", " Enter 6 to sort on Flighttime",
        " Enter 7 to sort on Departtime", " Enter 8 to sort on Arrivaltime", " Enter 9 to sort on Gate" };
        Console.WriteLine("\n+-------------------------+");
        foreach (string item in main_account_choices) Console.WriteLine(item);

        Console.WriteLine("+-------------------------+");
        Console.Write(">> ");
        string sort_choice = Console.ReadLine()!;
        FlightInfoLogic flight = new();
        switch (sort_choice)
        {
            case "1":
                _flights = flight.SortByFlightNumber();
                FlightSchedule();
                break;
            case "2":
                _flights = flight.SortByAircraft();
                FlightSchedule();
                break;
            case "3":
                _flights = flight.SortByOrigin();
                FlightSchedule();
                break;
            case "4":
                _flights = flight.SortByDestination();
                FlightSchedule();
                break;
            case "5":
                _flights = flight.SortByDate();
                FlightSchedule();
                break;
            case "6":
                _flights = flight.SortByFlightTime();
                FlightSchedule();
                break;
            case "7":
                _flights = flight.SortByDepartTime();
                FlightSchedule();
                break;
            case "8":
                _flights = flight.SortByArrivalTime();
                FlightSchedule();
                break;
            case "9":
                _flights = flight.SortByGate();
                FlightSchedule();
                break;
            default:
                break;
        }
    }
    public static void FlightSchedule()
    {

        ConsoleTable FlightTable = new ConsoleTable("Flight Number",
        "Aircraft",
        "Origin",
        "Destination",
        "Date",
        "FlightTime",
        "DepartTime",
        "ArrivalTime",
        "Gate");
        FlightTable.Options.EnableCount = options.EnableCount;
        for (int count = 0; count < _flights!.Count; count++)
        {
            FlightTable.AddRow(_flights[count].FlightNumber, _flights[count].Aircraft, _flights[count].Origin, _flights[count].Destination, _flights[count].Date, _flights[count].FlightTime,
            _flights[count].DepartTime, _flights[count].ArrivalTime, _flights[count].Gate);
        }
        Console.Clear();
        Console.WriteLine("All available flights: \n");
        Console.WriteLine(FlightTable.ToString());
        Console.WriteLine("Do you want to sort for easier viewing");
        Console.Write(">> ");
        string sorting = Console.ReadLine()!.ToLower();
        if (sorting == "yes" || sorting == "y") SortingMenu();
        else if (sorting == "no" || sorting == "n")
        {
            Console.WriteLine("Which flight do you want to fly on?");
            Console.Write(">> ");
            string flight_picker = Console.ReadLine()!;
            LayoutPlane();
        }
        else FlightSchedule();
    }
    public static void LayoutPlane()
    {
        ConsoleTable Table = new ConsoleTable("Row", "", "", "", "", "", "", "", "");
        Table.Options.EnableCount = options.EnableCount;
        for (int row = 1; row < 31; row++)
        {
            Table.AddRow("", "A" + row, "B" + row, "", "C" + row, "D" + row, "", "E" + row, "F" + row);
        }
        Console.Clear();
        Console.WriteLine("The Layout of the plane: \n");
        Console.WriteLine(Table.ToString());
    }








    // Create method filter flight by catagory

    // Create mehtod view flight information
}