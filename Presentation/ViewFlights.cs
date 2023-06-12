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
    public static int numberOfChildren = 0;

    public static int numberOfAdults = 0;

    public static int FlightID { get; private set; }

    public static void FlightMenu()
    {
        if (_flights == null)
            _flights = FlightInfoAccess.LoadAll();

        FlightInfoLogic Fil = new FlightInfoLogic();
        FlightSchedule();
        LayoutPlane();

        int FlightId = FlightID - 1;

        var FilterByFlightID = from s in _flights
                               where s.FlightID == FlightID - 1
                               select s;

        List<FlightInfoModel> _flight = FilterByFlightID.ToList();
        List<string> taken_seats = _flight[0].SeatsTaken;

        if (FlightID <= 0 || FlightID > _flights.Count)
        {
            AccountFunctionality.ErrorMessage("This Flight ID Does not exist, please try again...");
            FlightMenu();
        }
        else
        {
            AccountModel AccountInfo = UserLogin.AccountInfo!;
            bool valid_seat = false;
            List<string> selectedSeats = new List<string>();

            while (valid_seat == false)
            {
                Console.Write("Do you have a disability?\n>> ");
                string hasdisability = Console.ReadLine()!;
                if (hasdisability == "Y" || hasdisability == "y" || hasdisability == "Yes" || hasdisability == "yes")
                    HasDisability = true;

                Console.Write("Do you have any children (Y/N)? ");
                string hasChildren = Console.ReadLine()!;
                if (hasChildren == "Y" || hasChildren == "y" || hasChildren == "Yes" || hasChildren == "yes")
                {
                    Console.Write("How many children do you have? ");
                    int childrenCountInput = Convert.ToInt32(Console.ReadLine());
                    numberOfChildren = childrenCountInput;
                }

                Console.Write("How many adults (excluding children) are booking the flight? ");
                numberOfAdults = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine($"You have entered {numberOfAdults} adults and {numberOfChildren} children. Do you want to proceed with these numbers? (Y/N)");
                string proceedConfirmation = Console.ReadLine()!;
                if (proceedConfirmation != "Y" && proceedConfirmation != "y" && proceedConfirmation != "Yes" && proceedConfirmation != "yes")
                {
                    Console.WriteLine("Aborting booking. Please try again!");
                    FlightMenu();
                }

                Console.WriteLine("Choose the seat(s) you would like (separated by commas, e.g., A1, B2): ");
                string seatPicker = Console.ReadLine()!;
                selectedSeats = seatPicker.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

                List<char> PlaneRows = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F' };

                bool allSeatsValid = true;
                if (allSeatsValid)
                {
                    bool seatsAvailable = true;
                    foreach (string seat in selectedSeats)
                    {
                        if (_flight[0].SeatsTaken.Contains(seat))
                        {
                            AccountFunctionality.ErrorMessage($"Seat {seat} is already taken. Please choose another seat.");
                            seatsAvailable = false;
                            break;
                        }
                    }

                    if (seatsAvailable)
                    {
                        valid_seat = true;
                        SeatPicker = seatPicker;
                    }
                    else
                    {
                        valid_seat = false;
                        AccountFunctionality.ErrorMessage("One or more selected seats are not available. Please choose different seats.");
                    }
                }
            }

            double Ticket_Price = 0.0;
            List<string> firstClassSeats = new List<string> { "A1", "A2", "A3", "A4", "A5", "A6", "B1", "B2", "B3", "B4", "B5", "B6" };
            List<string> disabledSeats = new List<string> { "C1", "C2", "C3", "C4", "C5", "C6" };

            foreach (string seat in selectedSeats)
            {
                double ticketPrice = _flight[0].Price;

                if (firstClassSeats.Contains(seat))
                {
                    ticketPrice *= 2;
                }

                if (disabledSeats.Contains(seat) && !HasDisability)
                {
                    AccountFunctionality.ErrorMessage("You have no disability, so you cannot choose this seat. Please try again!");
                    FlightMenu();
                }

                if (numberOfChildren > 0)
                {
                    ticketPrice *= 0.8;
                    numberOfChildren--;
                }

                Ticket_Price += ticketPrice;
            }

            Console.Clear();
            Console.WriteLine("Please confirm your booking!");

            Console.WriteLine($"-----------");
            Console.WriteLine($"Flight: {_flight[0].FlightNumber} from {_flight[0].Origin} to {_flight[0].Destination}");
            Console.WriteLine($"Date : {_flight[0].Date} - {_flight[0].DepartTime}");
            Console.WriteLine($"Total price: {Ticket_Price}");
            Console.WriteLine($"Booked seat(s): {SeatPicker}");
            Console.WriteLine($"Number of adults: {numberOfAdults}");
            Console.WriteLine($"Number of children: {numberOfChildren}");
            Console.WriteLine("------------");
            Console.WriteLine("\n\nEnter Yes to confirm");
            string user_input = Console.ReadLine()!;
            if (user_input != "Yes" && user_input != "yes" && user_input != "Y" && user_input != "y")
            {
                Console.WriteLine("Abort booking, please wait 5 seconds and try again later!");
                Thread.Sleep(5000);
                FlightMenu();
            }

            _flight[0].SeatsTaken.AddRange(selectedSeats);
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
                // PrintFlightTable(possible_flights);
                break;
            case "2":
                _flights = flight.SortByAircraft(ascending);
                break;
            case "3":
                _flights = flight.SortByOrigin(ascending);
                break;
            case "4":
                _flights = flight.SortByDestination(ascending);
                break;
            case "5":
                _flights = flight.SortByDate(ascending);
                break;
            case "6":
                _flights = flight.SortByFlightTime(ascending);
                break;
            case "7":
                _flights = flight.SortByDepartTime(ascending);
                break;
            case "8":
                _flights = flight.SortByArrivalTime(ascending);
                break;
            case "9":
                _flights = flight.SortByGate(ascending);
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
        SortedDictionary<string, List<string>> destinationDates = new SortedDictionary<string, List<string>>();

        foreach (FlightInfoModel flight in _flights)
        {
            possible_destinations.Add(flight.Destination);

            if (!destinationDates.ContainsKey(flight.Destination))
            {
                destinationDates[flight.Destination] = new List<string>();
            }

            destinationDates[flight.Destination].Add(flight.Date);
        }

        Console.Clear();
        Console.WriteLine("Possible destinations: ");

        int currentDestinationIndex = 0;
        int destinationsPerPage = 25;
        int totalPages = (int)Math.Ceiling((double)possible_destinations.Count / destinationsPerPage);

        bool showMoreDestinations = true;

        while (showMoreDestinations)
        {
            currentDestinationIndex = currentDestinationIndex < 0 ? 0 : currentDestinationIndex;
            int destinationIndex = 1;
            Dictionary<int, string> destinationDictionary = new Dictionary<int, string>();

            for (int i = currentDestinationIndex; i < currentDestinationIndex + destinationsPerPage && i < possible_destinations.Count; i++)
            {
                string destination = possible_destinations.ElementAt(i);
                destinationDictionary.Add(destinationIndex, destination);
                Console.WriteLine($"- {destinationIndex}: {destination}");
                destinationIndex++;
            }

            Console.WriteLine($"Page {currentDestinationIndex / destinationsPerPage + 1} of {totalPages}");
            Console.WriteLine("Enter 'n' to view the next page of destinations, 'b' to view the previous page, or any other key to select a destination:");
            Console.Write(">> ");

            string destinationChoice = Console.ReadLine()!;

            if (destinationChoice.ToLower() == "n")
            {
                currentDestinationIndex += destinationsPerPage;
                if (currentDestinationIndex >= possible_destinations.Count)
                {
                    currentDestinationIndex = (totalPages - 1) * destinationsPerPage;
                    Console.Clear();
                    Console.WriteLine("This is the last page of destinations.");
                }
                else
                {
                    Console.Clear();
                }
            }
            else if (destinationChoice.ToLower() == "b")
            {
                currentDestinationIndex -= destinationsPerPage;
                if (currentDestinationIndex < 0)
                {
                    currentDestinationIndex = 0;
                    Console.Clear();
                    Console.WriteLine("This is the beginning of destinations.");
                }
                else
                {
                    Console.Clear();
                }
            }
            else
            {
                if (int.TryParse(destinationChoice, out int destinationID))
                {
                    if (destinationID >= 1 && destinationID <= destinationDictionary.Count)
                    {
                        string preferredDestination = destinationDictionary[destinationID];

                        if (destinationDates.ContainsKey(preferredDestination))
                        {
                            List<string> possible_dates = destinationDates[preferredDestination];
                            possible_dates.Sort((x, y) => DateTime.ParseExact(x, "dd-MM-yyyy", null).CompareTo(DateTime.ParseExact(y, "dd-MM-yyyy", null)));

                            Console.WriteLine("Possible dates for the selected destination: ");

                            int datesPerPage = 25;
                            int totalDatePages = (int)Math.Ceiling((double)possible_dates.Count / datesPerPage);
                            int currentDatePage = 1;

                            bool showMoreDates = true;

                            while (showMoreDates)
                            {
                                currentDatePage = currentDatePage < 1 ? 1 : currentDatePage;
                                int dateIndex = 1;
                                Dictionary<int, string> dateDictionary = new Dictionary<int, string>();

                                foreach (string date in possible_dates.Skip((currentDatePage - 1) * datesPerPage).Take(datesPerPage).Distinct())
                                {
                                    dateDictionary.Add(dateIndex, date);
                                    Console.WriteLine($"- {dateIndex}: {date}");
                                    dateIndex++;
                                }

                                Console.WriteLine($"Page {currentDatePage} of {totalDatePages}");
                                Console.WriteLine("Enter 'n' to view the next page of dates, 'b' to view the previous page, or any other key to select a date:");
                                Console.Write(">> ");

                                string dateChoice = Console.ReadLine()!;

                                if (dateChoice.ToLower() == "n")
                                {
                                    currentDatePage++;
                                    if (currentDatePage > totalDatePages)
                                    {
                                        currentDatePage = totalDatePages;
                                        Console.Clear();
                                        Console.WriteLine($"Showing dates page {currentDatePage} of {totalDatePages}");
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine($"Showing dates page {currentDatePage} of {totalDatePages}");
                                    }
                                }
                                else if (dateChoice.ToLower() == "b")
                                {
                                    currentDatePage--;
                                    if (currentDatePage < 1)
                                    {
                                        currentDatePage = 1;
                                        Console.Clear();
                                        Console.WriteLine($"Showing dates page {currentDatePage} of {totalDatePages}");
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine($"Showing dates page {currentDatePage} of {totalDatePages}");
                                    }
                                }
                                else
                                {
                                    if (int.TryParse(dateChoice, out int dateID))
                                    {
                                        if (dateID >= 1 && dateID <= dateDictionary.Count)
                                        {
                                            string selectedDate = dateDictionary[dateID];

                                            List<FlightInfoModel> possible_flights = _flights
                                                .Where(flight => flight.Destination == preferredDestination && flight.Date == selectedDate)
                                                .ToList();

                                            possible_flights.Sort((x, y) => DateTime.ParseExact(x.Date, "dd-MM-yyyy", null).CompareTo(DateTime.ParseExact(y.Date, "dd-MM-yyyy", null)));

                                            PrintFlightTable(possible_flights);

                                            // Console.WriteLine("Do you want to sort for easier viewing");
                                            // Console.Write(">> ");
                                            // string sorting = Console.ReadLine()!.ToLower();
                                            // if (sorting == "yes" || sorting == "y") SortingMenu(possible_flights);


                                            Console.WriteLine("What is the flight ID of the flight you will be taking?");
                                            Console.Write(">> ");
                                            try
                                            {
                                                FlightID = Convert.ToInt32(Console.ReadLine());
                                                showMoreDestinations = false;
                                                showMoreDates = false;
                                                break;
                                            }
                                            catch
                                            {
                                                Console.WriteLine("FlightID can only be a number!");
                                                continue;
                                            }

                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid date choice. Please try again.");
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input. Please try again.");
                                        continue;
                                    }
                                }
                            }
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
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }
            }
        }
    }



    public static void LayoutPlane()
    {
        if (_flights == null) _flights = FlightInfoAccess.LoadAll();
        var Table = new ConsoleTable("Row", "A", "B", "C", "D", "E", "F");
        Table.Options.EnableCount = options.EnableCount;

        var FilterByFlightID = from s in _flights
                               where s.FlightID == (FlightID - 1)
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
                if (_flights == null)
                    _flights = FlightInfoAccess.LoadAll();

                Console.WriteLine("Enter the flight ID you want to delete:");
                int removed_flight_id = Convert.ToInt32(Console.ReadLine()) - 1;

                Console.WriteLine("Enter the seat(s) of the booking you want to delete (separated by commas):");
                string seatsInput = Console.ReadLine()!;
                List<string> seatList = seatsInput.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

                var FilterByFlightID = from s in _flights
                                       where s.FlightID == removed_flight_id
                                       select s;

                List<FlightInfoModel> remove_flight = FilterByFlightID.ToList();

                bool bookingDeleted = false;

                foreach (List<string> flight in flight_account_info)
                {
                    if (flight.Contains(removed_flight_id.ToString()) && seatList.Any(flight.Contains))
                    {
                        // Flight section
                        foreach (string seat in seatList)
                        {
                            remove_flight[0].SeatsTaken.Remove(seat);
                        }
                        _flights[removed_flight_id] = remove_flight[0];
                        FlightInfoAccess.WriteAll(_flights);

                        // Account section
                        List<List<string>> list_flight_account_info = flight_account_info.ToList();
                        list_flight_account_info.RemoveAll(innerList => innerList.Contains(removed_flight_id.ToString()) && seatList.Any(innerList.Contains));
                        UserLogin.AccountInfo.BookedFlights = list_flight_account_info;
                        AccountsAccess.WriteAll(accountList);
                        bookingDeleted = true;
                        break;
                    }
                }

                if (bookingDeleted)
                {
                    Console.WriteLine("Booking deleted successfully!");
                }
                else
                {
                    Console.WriteLine("No booking found with the specified flight ID and seat(s).");
                }
            }
            else if (change_seat == true)
            {
                if (_flights == null)
                    _flights = FlightInfoAccess.LoadAll();

                Console.WriteLine("Enter the flight ID you want to change seats on:");
                int changed_seat_flightID = Convert.ToInt32(Console.ReadLine()) - 1;

                Console.WriteLine("Enter your old seat(s) (separated by commas):");
                string old_seatsInput = Console.ReadLine()!;
                List<string> old_seatList = old_seatsInput.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

                Console.WriteLine("Enter your new seat(s) (separated by commas):");
                string new_seatsInput = Console.ReadLine()!;
                List<string> new_seatList = new_seatsInput.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

                var FilterByFlightID = from s in _flights
                                       where s.FlightID == changed_seat_flightID
                                       select s;

                List<FlightInfoModel> ChangeSeatFlight = FilterByFlightID.ToList();

                bool seatsChanged = false;

                foreach (List<string> flight in flight_account_info)
                {
                    string[] bookedSeats = flight[3].Split(',', StringSplitOptions.RemoveEmptyEntries);

                    if (flight[0] == changed_seat_flightID.ToString() && old_seatsInput == flight[3])
                    {
                        // Flight section
                        foreach (string old_seat in old_seatList)
                        {
                            ChangeSeatFlight[0].SeatsTaken.RemoveAll(seat => seat == old_seat);
                        }
                        ChangeSeatFlight[0].SeatsTaken.AddRange(new_seatList);
                        _flights[changed_seat_flightID] = ChangeSeatFlight[0];
                        FlightInfoAccess.WriteAll(_flights);

                        // Account section
                        List<List<string>> list_flight_account_info = flight_account_info.ToList();
                        flight[3] = new_seatsInput;
                        list_flight_account_info.RemoveAll(innerList => innerList.Contains(changed_seat_flightID.ToString()) && innerList.Contains(old_seatsInput));
                        UserLogin.AccountInfo.BookedFlights = list_flight_account_info;
                        AccountsAccess.WriteAll(accountList);
                        seatsChanged = true;
                        break;
                    }
                }

                if (seatsChanged)
                {
                    Console.WriteLine("Seats changed successfully!");
                    UserLogin.AccountInfo.BookedFlights = flight_account_info;
                    AccountsAccess.WriteAll(accountList);
                }
                else
                {
                    Console.WriteLine("No booking found with the specified flight ID and old seat(s).");
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