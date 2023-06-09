using System;
using System.Data;
using ConsoleTables;
using DataModels;

public class ViewFlights
{
    public static List<FlightInfoModel>? _flights;

    public static List<AccountModel> accountList = AccountsAccess.LoadAll();

    public static ConsoleTableOptions options = new ConsoleTableOptions
    {
        EnableCount = false
    };
    public static string? SeatPicker;

    public static bool HasDisability = false;

    public static int FlightID { get; private set; }
    public static void FlightMenu()
    {
        if (_flights == null) _flights = FlightInfoAccess.LoadAll();
        FlightInfoLogic Fil = new FlightInfoLogic();
        FlightSchedule();
        LayoutPlane();
        int FlightId = FlightID - 1;

        if (FlightID <= 0 || FlightID > _flights.Count)
        {
            Console.WriteLine("This Flight ID Does not exist please try again...");
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
            FlightMenu();
        }
        else
        {
            AccountModel AccountInfo = UserLogin.AccountInfo!;
            bool valid_seat = false;
            while (valid_seat == false)
            {
                Console.Write("Do you have a disability\n>> ");
                string hasdisability = Console.ReadLine()!;
                if (hasdisability == "Y" || hasdisability == "y" || hasdisability == "Yes" || hasdisability == "yes") HasDisability = true;

                Console.WriteLine("Choose a seat you would like");
                string seat_picker = Console.ReadLine()!;
                List<char> PlaneRows = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F' };
                if (PlaneRows.Contains(seat_picker[0]))
                {
                    if (Convert.ToInt32(seat_picker[1..]) >= 1 && Convert.ToInt32(seat_picker[1..]) <= 30)
                    {
                        valid_seat = true;
                        SeatPicker = seat_picker;
                    }
                    else
                    {
                        AccountFunctionality.ErrorMessage("This seat is not available, please try again");
                    }
                }
                else
                {
                    AccountFunctionality.ErrorMessage("This seat is not available, please try again");
                }
            }

            if (_flights == null) _flights = FlightInfoAccess.LoadAll();
            var FilterByFlightID = from s in _flights
                                   where s.FlightID == FlightId
                                   select s;

            List<FlightInfoModel> _flight = FilterByFlightID.ToList();
            List<string> taken_seats = _flight[0].SeatsTaken;

            foreach (string taken_seat in taken_seats)
            {
                if (taken_seat == SeatPicker)
                {
                    Console.WriteLine("This seat is already taken, please wait and try again in 5 seconds");
                    Thread.Sleep(5000);
                    ViewFlights.FlightMenu();
                }
            }

            List<string> first_class_seats = new List<string> { "A1", "A2", "A3", "A4", "A5", "A6", "B1", "B2", "B3", "B4", "B5", "B6" };
            List<string> disabled_seats = new List<string> { "C1", "C2", "C3", "C4", "C5", "C6" };
            double Ticket_Price = _flight[0].Price;
            if (first_class_seats.Contains(SeatPicker))
            {
                double ticket_price = _flight[0].Price * 2;
                Ticket_Price = ticket_price;

            }
            if (disabled_seats.Contains(SeatPicker))
            {
                if (!HasDisability)
                {
                    Console.WriteLine("You have no disability so you cannot choose this seat, please try again!");
                    FlightMenu();
                }
            }
            Console.Clear();
            Console.WriteLine("Please confirm your booking!");

            Console.WriteLine($"-----------");
            Console.WriteLine($"Flight: {_flight[0].FlightNumber} from {_flight[0].Origin} to {_flight[0].Destination}");
            Console.WriteLine($"Date : {_flight[0].Date} - {_flight[0].DepartTime}");
            Console.WriteLine($"Total price: {Ticket_Price}");
            Console.WriteLine($"Booked seat: {SeatPicker}");
            Console.WriteLine("------------");
            Console.WriteLine("\n\nEnter Yes to confirm");
            string user_input = Console.ReadLine()!;
            if (user_input != "Yes" && user_input != "yes" && user_input != "Y" && user_input != "y")
            {
                Console.WriteLine("Abort booking, please wait 5 seconds and try again later!");
                Thread.Sleep(5000);
                FlightMenu();
            }


            _flight[0].SeatsTaken.Add(SeatPicker!);
            _flights[FlightId] = _flight[0];
            FlightInfoAccess.WriteAll(_flights);

            int highestId = FilterByFlightID.Max(data => data.FlightID);
            BookHistoryModel newData = new BookHistoryModel(AccountInfo.Id, AccountInfo.FullName, AccountInfo.EmailAddress,
            DateTime.Now.ToString(), FlightId, _flight[0].FlightNumber, SeatPicker!, _flight[0].Destination, _flight[0].Gate, _flight[0].DepartTime, _flight[0].ArrivalTime);

            List<BookHistoryModel> dataList = BookHistoryAccess.LoadAll();
            dataList.Add(newData);
            BookHistoryAccess.WriteAll(dataList);

            AccountModel? updatedAccount = accountList.FirstOrDefault(a => a.Id == AccountInfo.Id);
            if (updatedAccount != null)
            {
                List<string> updatedBookedFlights = new List<string> {
                    FlightId.ToString(),
                    _flight[0].FlightNumber,
                    DateTime.Now.ToString(),
                    SeatPicker!,
                    _flight[0].Origin,
                    _flight[0].Destination,
                    _flight[0].DepartTime.ToString(),
                    _flight[0].ArrivalTime.ToString(),
                    Ticket_Price.ToString(),
                    _flight[0].Gate,
                };

                updatedAccount.BookedFlights.Add(updatedBookedFlights);
                UserLogin.AccountInfo = updatedAccount;
                AccountsAccess.WriteAll(accountList);
            }
            Menu.Account();
        }
    }

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
    public static void PrintFlightTable(List<FlightInfoModel> flight_list)
    {
        ConsoleTable FlightTable = new ConsoleTable("FlightID", "Flight Number",
        "Aircraft",
        "Origin",
        "Destination",
        "Date",
        "FlightTime",
        "DepartTime",
        "ArrivalTime",
        "Price",
        "Gate");
        FlightTable.Options.EnableCount = options.EnableCount;
        for (int count = 0; count < flight_list!.Count; count++)
        {
            FlightTable.AddRow(flight_list[count].FlightID + 1, flight_list[count].FlightNumber, flight_list[count].Aircraft, flight_list[count].Origin, flight_list[count].Destination, flight_list[count].Date, flight_list[count].FlightTime,
            flight_list[count].DepartTime, flight_list[count].ArrivalTime, flight_list[count].Price, flight_list[count].Gate);
        }
        Console.Clear();
        Console.WriteLine("All available flights: \n");
        Console.WriteLine(FlightTable.ToString());
    }
    public static void FlightSchedule()
    {
        if (_flights == null)
            _flights = FlightInfoAccess.LoadAll();

        HashSet<string> possible_destinations = new HashSet<string>();
        Dictionary<string, HashSet<string>> destinationDates = new Dictionary<string, HashSet<string>>();

        foreach (FlightInfoModel flight in _flights)
        {
            possible_destinations.Add(flight.Destination);

            if (!destinationDates.ContainsKey(flight.Destination))
            {
                destinationDates[flight.Destination] = new HashSet<string>();
            }

            destinationDates[flight.Destination].Add(flight.Date);
        }

        Console.Clear();
        Console.WriteLine("Possible destinations: ");
        int destinationIndex = 1;
        Dictionary<int, string> destinationDictionary = new Dictionary<int, string>();
        foreach (string destination in possible_destinations)
        {
            destinationDictionary.Add(destinationIndex, destination);
            Console.WriteLine($"- {destinationIndex}: {destination}");
            destinationIndex++;
        }

        while (true)
        {
            Console.WriteLine("Please enter your preferred destination");
            Console.Write(">> ");
            int destinationID = Convert.ToInt32(Console.ReadLine());

            if (destinationID >= 1 && destinationID <= destinationDictionary.Count)
            {
                string preferredDestination = destinationDictionary[destinationID];

                if (destinationDates.ContainsKey(preferredDestination))
                {
                    HashSet<string> possible_dates = destinationDates[preferredDestination];

                    Console.WriteLine("Possible dates for the selected destination: ");
                    int dateIndex = 1;
                    Dictionary<int, string> dateDictionary = new Dictionary<int, string>();
                    foreach (string date in possible_dates)
                    {
                        dateDictionary.Add(dateIndex, date);
                        Console.WriteLine($"- {dateIndex}: {date}");
                        dateIndex++;
                    }

                    Console.WriteLine("Please enter the desired date:");
                    Console.Write(">> ");

                    int dateChoice = Convert.ToInt32(Console.ReadLine());

                    if (dateDictionary.ContainsKey(dateChoice))
                    {
                        string selectedDate = dateDictionary[dateChoice];

                        List<FlightInfoModel> possible_flights = _flights
                            .Where(flight => flight.Destination == preferredDestination && flight.Date == selectedDate)
                            .ToList();

                        PrintFlightTable(possible_flights);
                    }
                    else
                    {
                        Console.WriteLine("Invalid date choice. Please try again.");
                        continue;
                    }

                    Console.WriteLine("What is the flight ID of the flight you will be taking?");
                    Console.Write(">> ");
                    try
                    {
                        FlightID = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("FlightID can only be a number!");
                        continue;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("No flights available for the selected destination. Please try again.");
                    continue;
                }
            }
            else
            {
                Console.WriteLine("Invalid destination number. Please try again.");
            }
        }

        Console.Clear();
        Console.WriteLine("This destination does not exist. Please try again!");
        Console.WriteLine("Possible destinations: ");
        foreach (string destination in possible_destinations)
        {
            Console.WriteLine($"- {destination}");
        }
    }



    public static void LayoutPlane()
    {
        if (_flights == null) _flights = FlightInfoAccess.LoadAll();
        var Table = new ConsoleTable("Row", "A", "B", "C", "D", "E", "F");
        Table.Options.EnableCount = options.EnableCount;

        var FilterByFlightID = from s in _flights
                               where s.FlightID == FlightID - 1
                               select s;

        List<FlightInfoModel> remove_flight = FilterByFlightID.ToList();
        List<string> taken_seats = remove_flight[0].SeatsTaken;

        for (int row = 1; row < 31; row++)
        {
            var table_row = new List<string> { row.ToString() };

            for (char columnChar = 'A'; columnChar <= 'F'; columnChar++)
            {
                string seat = $"{columnChar}{row}";
                string value = taken_seats.Contains(seat) ? "X" : "";
                table_row.Add(value);
            }

            Table.AddRow(table_row.ToArray());
        }

        Console.Clear();
        Console.WriteLine("The Layout of the plane: \n");
        Table.Write();
    }
    public static void SeeBookings(bool delete_flight, bool change_seat)
    {
        int AmountOfFlights = UserLogin.AccountInfo!.BookedFlights.Count;
        List<List<string>> flight_account_info = new List<List<string>>(UserLogin.AccountInfo.BookedFlights);


        if (AmountOfFlights == 0)
        {
            Console.WriteLine("No booked flights yet!");
        }
        else
        {
            ConsoleTable Bookingtable = new ConsoleTable("FlightID", "Flight Number",
            "Date",
            "Seat",
            "Origin",
            "Destination",
            "DepartTime",
            "ArrivalTime",
            "Price",
            "Gate");
            Bookingtable.Options.EnableCount = options.EnableCount;
            foreach (List<string> booking in flight_account_info)
            {
                Bookingtable.AddRow(Convert.ToInt32(booking[0]) + 1, booking[1], booking[2], booking[3], booking[4], booking[5], booking[6], booking[7], booking[8], booking[9]);
            }
            Console.Clear();
            Console.WriteLine("All booked flights: \n");
            Console.WriteLine(Bookingtable.ToString());

            if (delete_flight == false && change_seat == false)
            {
                Console.WriteLine("Do you want to manage your flights? Enter Y/y/Yes/yes");
                string user_input = Console.ReadLine()!;
                if (user_input == "Y" || user_input == "y" || user_input == "Yes" || user_input == "yes")
                {
                    Menu.ManageBookings();
                }
            }
            else if (delete_flight == true)
            {
                if (_flights == null) _flights = FlightInfoAccess.LoadAll();
                Console.WriteLine("Enter your flight ID you want to delete");
                string removed_flight_id = Convert.ToString(Convert.ToInt32(Console.ReadLine()) - 1);
                Console.WriteLine("The seat of the booking you want to delete");
                string seat = Console.ReadLine()!;

                var FilterByFlightID = from s in _flights
                                       where s.FlightID == Convert.ToInt32(removed_flight_id)
                                       select s;

                List<FlightInfoModel> remove_flight = FilterByFlightID.ToList();

                foreach (List<string> flight in flight_account_info)
                {
                    if (flight.Contains(removed_flight_id) && flight.Contains(seat))
                    {
                        // Flight section
                        remove_flight[0].SeatsTaken.Remove(flight[3]);
                        _flights![Convert.ToInt32(removed_flight_id)] = remove_flight[0];
                        FlightInfoAccess.WriteAll(_flights);

                        // Account section
                        List<List<string>> list_flight_account_info = flight_account_info.ToList();
                        list_flight_account_info.RemoveAll(innerList => innerList.Contains(removed_flight_id) && innerList.Contains(seat));
                        UserLogin.AccountInfo.BookedFlights = list_flight_account_info;
                        AccountsAccess.WriteAll(accountList);



                        // Impelement code to remove flight from json (by flightID)! 

                        Console.WriteLine("Flight deleted successfully!");
                    }
                }
            }
            else if (change_seat == true)
            {
                if (_flights == null) _flights = FlightInfoAccess.LoadAll();
                Console.WriteLine("Enter your flight ID you want to change seats on");
                int changed_seat_flightID = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter your old seat you want to change seats on");
                string old_seat = Console.ReadLine()!;

                Console.WriteLine("Enter your new seat you want to change seats on");
                string new_seat = Console.ReadLine()!;

                var FilterByFlightID = from s in _flights
                                       where s.FlightID == Convert.ToInt32(changed_seat_flightID)
                                       select s;

                List<FlightInfoModel> ChangeSeatFlight = FilterByFlightID.ToList();

                foreach (List<string> flight in flight_account_info)
                {
                    if (flight.Contains(old_seat))
                    {
                        // Flight section
                        ChangeSeatFlight[0].SeatsTaken.Remove(old_seat);
                        ChangeSeatFlight[0].SeatsTaken.Add(new_seat);
                        _flights[changed_seat_flightID] = ChangeSeatFlight[0];
                        FlightInfoAccess.WriteAll(_flights);

                        // Account section
                        List<List<string>> list_flight_account_info = flight_account_info.ToList();
                        flight[3] = new_seat;
                        UserLogin.AccountInfo.BookedFlights = list_flight_account_info;
                        AccountsAccess.WriteAll(accountList);

                        // Implement code for changing seat (by flightID)! 

                        Console.WriteLine("Seat changed successfully");
                    }
                }
            }
        }
        Console.WriteLine("Press any key to go back to the menu.");
        Console.ReadKey();
        Menu.Account();
        // Create method filter flight by catagory

        // Create method view flight information
    }
}