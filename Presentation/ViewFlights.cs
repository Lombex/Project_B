using ConsoleTables;
using DataModels;


// everything is static, can we just make the class static?
public class ViewFlights
{
    private static List<FlightInfoModel>? _flights;

    public static ConsoleTableOptions options = new ConsoleTableOptions
    {
        EnableCount = false
    };

    public static int FlightID { get; private set; }

    public static void FlightMenu()
    {
        if (_flights == null) _flights = FlightInfoAccess.LoadAll();
        FlightInfoLogic Fil = new FlightInfoLogic();
        FlightSchedule();
        LayoutPlane();
        Console.WriteLine("Choose a seat you would like");
        AccountModel AccountInfo = UserLogin.AccountInfo!;
        int FlightId = FlightID - 1;
        string SeatPicker = Console.ReadLine()!;

        var FilterByFlightID = from s in _flights
                               where s.FlightID == FlightId
                               select s;

        _flights = FilterByFlightID.ToList();

        int highestId = FilterByFlightID.Max(data => data.FlightID);
        BookHistoryModel newData = new BookHistoryModel(AccountInfo.Id, AccountInfo.FullName, AccountInfo.EmailAddress,
        DateTime.Now.ToString(), FlightId, _flights[0].FlightNumber, SeatPicker, _flights[0].Destination, _flights[0].Gate, _flights[0].DepartTime, _flights[0].ArrivalTime);

        List<BookHistoryModel> dataList = BookHistoryAccess.LoadAll();
        dataList.Add(newData);
        BookHistoryAccess.WriteAll(dataList);


        List<AccountModel> accountList = AccountsAccess.LoadAll();
        AccountModel? updatedAccount = accountList.FirstOrDefault(a => a.Id == AccountInfo.Id);
        if (updatedAccount != null)
        {
            List<string> updatedBookedFlights = new List<string>
            {
                FlightId.ToString(),
                _flights[0].FlightNumber,
                DateTime.Now.ToString(),
                SeatPicker,
                _flights[0].Origin,
                _flights[0].Destination,
                _flights[0].DepartTime.ToString(),
                _flights[0].ArrivalTime.ToString(),
                _flights[0].Gate,
            };
            updatedAccount.BookedFlights.Add(updatedBookedFlights);
            AccountsAccess.WriteAll(accountList);
        }
        Menu.Account();
    }


    // Set all needed items in book json :
    /*
    This is by AccountInfo
    ----------------------------------
        - Account ID
        - Account Name
        - Account Email
        - Book Time

    This is lookedup by the flight id
    ----------------------------------- 
        - Booked Airplane
        - Booked Seat
        - Booked Destination
        - Booked Gate
        - Flight Takeoff
        - Flight Arrival
    */

    // Check which class seat it has to be

    // Check if User has an disability or has children for discount.

    // Make sure to ask for confirmation 

    // Menu has be able to pick seats | Menu has to be able to go back to its respective menu


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
        bool ascending = true;

        Console.WriteLine("Enter 'asc' for ascending or 'desc' for descending:");
        string sort_preference = Console.ReadLine()!;
        if (sort_preference.ToLower() == "desc")
            ascending = false;

        switch (sort_choice)
        {
            case "1":
                _flights = flight.SortByFlightNumber(ascending);
                FlightSchedule();
                break;
            case "2":
                _flights = flight.SortByAircraft(ascending);
                FlightSchedule();
                break;
            case "3":
                _flights = flight.SortByOrigin(ascending);
                FlightSchedule();
                break;
            case "4":
                _flights = flight.SortByDestination(ascending);
                FlightSchedule();
                break;
            case "5":
                _flights = flight.SortByDate(ascending);
                FlightSchedule();
                break;
            case "6":
                _flights = flight.SortByFlightTime(ascending);
                FlightSchedule();
                break;
            case "7":
                _flights = flight.SortByDepartTime(ascending);
                FlightSchedule();
                break;
            case "8":
                _flights = flight.SortByArrivalTime(ascending);
                FlightSchedule();
                break;
            case "9":
                _flights = flight.SortByGate(ascending);
                FlightSchedule();
                break;
            default:
                break;
        }
    }
    public static void FlightSchedule()
    {

        ConsoleTable FlightTable = new ConsoleTable("FlightID", "Flight Number",
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
            FlightTable.AddRow(count + 1, _flights[count].FlightNumber, _flights[count].Aircraft, _flights[count].Origin, _flights[count].Destination, _flights[count].Date, _flights[count].FlightTime,
            _flights[count].DepartTime, _flights[count].ArrivalTime, _flights[count].Gate);
        }
        Console.Clear();
        Console.WriteLine("All available flights: \n");
        Console.WriteLine(FlightTable.ToString());
        Console.WriteLine("Would you like to enable sorting for a more organized viewing?");
        Console.Write(">> ");
        string sorting = Console.ReadLine()!.ToLower();
        if (sorting == "yes" || sorting == "y") SortingMenu();
        else if (sorting == "no" || sorting == "n")
        {
            Console.WriteLine("What is the flight ID of the flight you will be taking?");
            Console.Write(">> ");
            try
            {
                FlightID = Convert.ToInt32(Console.ReadLine()!);
            }
            catch
            {
                Console.WriteLine("FlightID can only be a number!");
                FlightSchedule();
            }
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