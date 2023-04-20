using ConsoleTables;
public class ViewFlights
{
    private static List<FlightInfoModel>? _flights;
    public static void Menu()
    {
        if (_flights == null)
        {
            _flights = FlightInfoAccess.LoadAll();
        }

        ConsoleTableOptions options = new ConsoleTableOptions
        {
            EnableCount = false
        };

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
        for (int count = 0; count < _flights.Count; count++)
        {
            FlightTable.AddRow(_flights[count].FlightNumber, _flights[count].Aircraft, _flights[count].Origin, _flights[count].Destination, _flights[count].Date, _flights[count].FlightTime,
            _flights[count].DepartTime, _flights[count].ArrivalTime, _flights[count].Gate);
        }
        Console.WriteLine("All available flights: \n");
        Console.WriteLine(FlightTable.ToString());



        // ConsoleTable Table = new ConsoleTable("Row", "", "", "", "", "", "", "", "");
        // Table.Options.EnableCount = options.EnableCount;
        // for (int row = 1; row < 31; row++)
        // {
        //     Table.AddRow("", "A" + row, "B" + row, "", "C" + row, "D" + row, "", "E" + row, "F" + row);
        // }

        // Console.WriteLine(Table.ToString());

    }
    // Create method filter flight by catagory

    // Create mehtod view flight information
}