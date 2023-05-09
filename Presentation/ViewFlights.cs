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
        FlightInfoLogic Fil = new FlightInfoLogic();
        Console.WriteLine("Add Filter (Yes or No)?");
        string input = Console.ReadLine().ToLower()!;

        if (input == "yes" || input == "y")
        {

            List<string> main_account_choices = new List<string>() { " Enter 1 to filter on Flightnumber", " Enter 2 to filter on Aircraft", " Enter 3 to filter on Origin",
            " Enter 4 to filter on Destination", " Enter 5 to filter on Date", " Enter 6 to filter on Flighttime",
            " Enter 7 to filter on Departtime", " Enter 8 to filter on Arrivaltime", " Enter 9 to filter on Gate" };
            Console.WriteLine("\n+-------------------------+");
            foreach (string item in main_account_choices) Console.WriteLine(item);

            Console.WriteLine("+-------------------------+");
            Console.Write(">> ");
            string filter_choice = Console.ReadLine()!;
            switch (filter_choice)
            {
                case "1":
                    Console.WriteLine("On which FlightNumber do you want to filter");
                    string Flightnumber = Console.ReadLine()!;
                    Fil.GetByFlightNumber(Flightnumber);
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
                case "6":
                    break;
                case "7":
                    break;
                case "8":
                    break;
                case "9":
                    break;
                default:
                    break;
            }
        }
        else if (input == "no" || input == "n")
        {
            FlightSchedule();
        }
        else ViewFlights.Menu();
    }

    public static void FlightSchedule()
    {
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
        for (int count = 0; count < _flights!.Count; count++)
        {
            FlightTable.AddRow(_flights[count].FlightNumber, _flights[count].Aircraft, _flights[count].Origin, _flights[count].Destination, _flights[count].Date, _flights[count].FlightTime,
            _flights[count].DepartTime, _flights[count].ArrivalTime, _flights[count].Gate);
        }
        Console.WriteLine("All available flights: \n");
        Console.WriteLine(FlightTable.ToString());
    }




    // ConsoleTable Table = new ConsoleTable("Row", "", "", "", "", "", "", "", "");
    // Table.Options.EnableCount = options.EnableCount;
    // for (int row = 1; row < 31; row++)
    // {
    //     Table.AddRow("", "A" + row, "B" + row, "", "C" + row, "D" + row, "", "E" + row, "F" + row);
    // }

    // Console.WriteLine(Table.ToString());

    // Create method filter flight by catagory

    // Create mehtod view flight information
}