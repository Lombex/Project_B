using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading;
using ConsoleTables;
using Project.Presentation;

public class ViewFlights
{

    private static AccountsAccess accountsAccess = new AccountsAccess();
    private static FlightInfoAccess flightinfoAccess = new FlightInfoAccess();

    private static BookHistoryAccess bookhistoryAccess = new BookHistoryAccess();

    public static List<FlightInfoModel>? _flights;

    public static List<AccountModel> accountList = accountsAccess.LoadAll();

    public static ConsoleTableOptions options = new ConsoleTableOptions
    {
        EnableCount = false
    };
    public static string? SeatPicker;

    public static bool HasDisability = false;
    public static int numberOfChildren;
    public static int numberOfAdults;
    public static int FlightID { get; private set; }

    public static void FlightMenu()
    {
        if (_flights == null)
            _flights = flightinfoAccess.LoadAll();

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
            double seat_price = _flight[0].Price;
            Console.WriteLine($"Seat price overview: ");
            Console.WriteLine($"-------------------------");
            Console.WriteLine($"Adult window first class seat: €{Math.Round((seat_price * _flight[0].FirstClassMutiplier) * _flight[0].WindowMultuplier)}\nAdult first class seat: €{Math.Round((seat_price * _flight[0].FirstClassMutiplier))}\nChildren window first class seat: €{Math.Round(((seat_price * _flight[0].FirstClassMutiplier) * 0.8) * _flight[0].WindowMultuplier)}\nChildren first class seat: €{Math.Round((seat_price * _flight[0].FirstClassMutiplier) * 0.8)}\nAdult window second class seat: €{Math.Round(seat_price * _flight[0].WindowMultuplier)}\nAdult second class seat: €{Math.Round(seat_price)}\nChildren window second class seat: €{Math.Round(seat_price * 0.8) * _flight[0].WindowMultuplier}\nChildren second class seat: €{Math.Round(seat_price * 0.8)}\nFor user with disability, choose row 3 for normal prices!");
            Console.WriteLine($"-------------------------");


            AccountModel AccountInfo = UserLogin.AccountInfo!;
            bool valid_seat = false;
            List<string> selectedAdultSeats = new List<string>();
            List<string> selectedChildSeats = new List<string>();
            List<string> allSelectedSeats = new List<string>();
            List<string> firstClassSeats = new List<string> { "A1", "A2", "B1", "B2", "C2", "D1", "D2", "E1", "E2", "F1", "F2" };
            List<string> disabledSeats = new List<string> { "A3", "B3", "C3", "D3", "E3", "F3" };
            List<string> windowSeats = new List<string>()
        {
            "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "A10",
            "A11", "A12", "A13", "A14", "A15", "A16", "A17", "A18", "A19", "A20",
            "A21", "A22", "A23", "A24", "A25", "A26", "A27", "A28", "A29", "A30",
            "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10",
            "F11", "F12", "F13", "F14", "F15", "F16", "F17", "F18", "F19", "F20",
            "F21", "F22", "F23", "F24", "F25", "F26", "F27", "F28", "F29", "F30"
        };

            while (!valid_seat)
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
                else
                {
                    numberOfChildren = 0;
                }

                Console.Write("How many adults (excluding children) are booking the flight? ");
                numberOfAdults = Convert.ToInt32(Console.ReadLine());

                if (numberOfAdults <= 0 && numberOfChildren <= 1)
                {
                    numberOfAdults = 0;
                    numberOfChildren = 1;
                    Console.WriteLine($"You have entered {numberOfChildren} child. Do you want to proceed with these numbers? (Y/N)");
                }
                else if (numberOfAdults == 1 && numberOfChildren == 0)
                {
                    Console.WriteLine($"You have entered {numberOfAdults} adult. Do you want to proceed with these numbers? (Y/N)");
                }
                else if (numberOfAdults <= 0 && numberOfChildren > 1)
                {
                    numberOfAdults = 0;
                    Console.WriteLine($"You have entered {numberOfChildren} children. Do you want to proceed with these numbers? (Y/N)");
                }
                else if (numberOfChildren == 0 && numberOfAdults > 1)
                {
                    Console.WriteLine($"You have entered {numberOfAdults} adults. Do you want to proceed with these numbers? (Y/N)");
                }
                else if (numberOfChildren == 1 && numberOfAdults == 1)
                {
                    Console.WriteLine($"You have entered {numberOfAdults} adult and {numberOfChildren} child. Do you want to proceed with these numbers? (Y/N)");
                }
                else
                {
                    Console.WriteLine($"You have entered {numberOfAdults} adults and {numberOfChildren} children. Do you want to proceed with these numbers? (Y/N)");
                }

                string proceedConfirmation = Console.ReadLine()!;
                if (proceedConfirmation != "Y" && proceedConfirmation != "y" && proceedConfirmation != "Yes" && proceedConfirmation != "yes")
                {
                    Console.WriteLine("Aborting booking. Please try again!");
                    FlightMenu();
                }

                Console.WriteLine("Choose the seat(s) you would like for adults (separated by commas, e.g., A1,B2): ");
                string adultSeatPicker = Console.ReadLine()!;
                selectedAdultSeats = adultSeatPicker.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

                if (numberOfChildren > 0)
                {
                    Console.WriteLine("Choose the seat(s) you would like for children (separated by commas, e.g., A1,B2): ");
                    string childSeatPicker = Console.ReadLine()!;
                    selectedChildSeats = childSeatPicker.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                }

                allSelectedSeats = selectedAdultSeats.Concat(selectedChildSeats).ToList();

                bool allSeatsValid = true;

                foreach (string seat in allSelectedSeats)
                {
                    if (!IsSeatValid(seat))
                    {
                        AccountFunctionality.ErrorMessage($"Seat {seat} is not valid. Please choose another seat.");
                        allSeatsValid = false;
                        break;
                    }
                }

                if (allSeatsValid)
                {
                    bool seatsAvailable = true;

                    foreach (string seat in allSelectedSeats)
                    {
                        if (taken_seats.Contains(seat))
                        {
                            AccountFunctionality.ErrorMessage($"Seat {seat} is already taken. Please choose another seat.");
                            seatsAvailable = false;
                            break;
                        }

                        if (disabledSeats.Contains(seat) && !HasDisability)
                        {
                            AccountFunctionality.ErrorMessage("You have no disability, so you cannot choose this seat. Please try again!");
                            FlightMenu();
                        }
                    }

                    if (seatsAvailable)
                    {
                        valid_seat = true;
                    }
                    else
                    {
                        valid_seat = false;
                        AccountFunctionality.ErrorMessage("One or more selected seats are not available. Please choose different seats.");
                    }
                }
                else
                {
                    valid_seat = false;
                    continue;
                }
            }

            Menu.CateringMenu();

            bool IsSeatValid(string seat)
            {
                List<string> valid_rows = new List<string> { "A", "B", "C", "D", "E", "F" };
                char row = seat[0];
                int column;
                if (int.TryParse(seat[1..], out column))
                {
                    return valid_rows.Contains(Convert.ToString(row)) && column >= 1 && column <= 30;
                }
                return false;
            }

            double Ticket_Price = 0.0;

            foreach (string seat in selectedAdultSeats)
            {
                double ticketPrice = _flight[0].Price;

                if (firstClassSeats.Contains(seat))
                {
                    ticketPrice *= _flight[0].FirstClassMutiplier;
                    if (windowSeats.Contains(seat))
                    {
                        ticketPrice *= _flight[0].WindowMultuplier;
                    }
                }

                Ticket_Price += ticketPrice;
            }

            foreach (string seat in selectedChildSeats)
            {
                double childTicketPrice = _flight[0].Price;

                if (firstClassSeats.Contains(seat))
                {
                    childTicketPrice *= 2;
                    childTicketPrice *= 0.8;
                    if (windowSeats.Contains(seat))
                    {
                        childTicketPrice *= _flight[0].WindowMultuplier;
                    }
                }
                else
                {
                    childTicketPrice *= 0.8;
                    if (windowSeats.Contains(seat))
                    {
                        childTicketPrice *= _flight[0].WindowMultuplier;
                    }

                }

                Ticket_Price += childTicketPrice;
            }

            double GetTotalTicketPrice = Ticket_Price + Menu.GetTotalOrderAmount(Menu.CateringOrders);

            Console.Clear();
            Console.WriteLine("Please confirm your booking!");

            Console.WriteLine($"-----------");
            Console.WriteLine($"Flight: {_flight[0].FlightNumber} from {_flight[0].Origin} to {_flight[0].Destination}");
            Console.WriteLine($"Date : {_flight[0].Date} - {_flight[0].DepartTime}");
            Console.WriteLine($"Total price: {Math.Round(GetTotalTicketPrice)}");
            Console.WriteLine($"Booked seat(s): {string.Join(",", allSelectedSeats)}");
            Console.WriteLine($"Number of adults: {numberOfAdults}");
            Console.WriteLine($"Number of children: {numberOfChildren}");
            Console.WriteLine($"Catering Orders: {string.Join(",", Menu.CateringOrders)}");
            Console.WriteLine("------------");
            Console.WriteLine("\n\nEnter Yes to confirm");
            string user_input = Console.ReadLine()!;
            if (user_input != "Yes" && user_input != "yes" && user_input != "Y" && user_input != "y")
            {
                Console.WriteLine("Abort booking, please wait 5 seconds and try again later!");
                Menu.CateringOrders.Clear();
                Thread.Sleep(5000);
                FlightMenu();
            }

            _flight[0].SeatsTaken.AddRange(selectedAdultSeats);
            _flight[0].SeatsTaken.AddRange(selectedChildSeats);
            _flights[FlightId] = _flight[0];
            flightinfoAccess.WriteAll(_flights);

            int highestId = FilterByFlightID.Max(data => data.FlightID);
            BookHistoryModel newData = new BookHistoryModel(AccountInfo.Id, AccountInfo.FullName, AccountInfo.EmailAddress,
                DateTime.Now.ToString(), FlightId, _flight[0].FlightNumber, SeatPicker!, _flight[0].Destination, _flight[0].Gate, _flight[0].DepartTime, _flight[0].ArrivalTime);

            List<BookHistoryModel> dataList = bookhistoryAccess.LoadAll();
            dataList.Add(newData);
            bookhistoryAccess.WriteAll(dataList);

            AccountModel? updatedAccount = accountList.FirstOrDefault(a => a.Id == AccountInfo.Id);
            if (updatedAccount != null)
            {
                List<string> updatedBookedFlights = new List<string> {
                    FlightId.ToString(),
                    _flight[0].FlightNumber,
                    DateTime.Now.ToString(),
                    string.Join(",", selectedAdultSeats.Concat(selectedChildSeats)),
                    _flight[0].Origin,
                    _flight[0].Destination,
                    _flight[0].DepartTime.ToString(),
                    _flight[0].ArrivalTime.ToString(),
                    Math.Round(GetTotalTicketPrice).ToString(),
                    string.Join(", ", Menu.CateringOrders),
                    _flight[0].Gate,
                };

                updatedAccount.BookedFlights.Add(updatedBookedFlights);
                UserLogin.AccountInfo = updatedAccount;
                accountsAccess.WriteAll(accountList);
                Menu.CateringOrders.Clear();
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
            _flights = flightinfoAccess.LoadAll();

        HashSet<string> possible_destinations = new HashSet<string>();
        SortedDictionary<string, List<string>> destinationDates = new SortedDictionary<string, List<string>>();

        DateTime currentDateTime = DateTime.Now;
        foreach (FlightInfoModel flight in _flights)
        {
            DateTime departureDateTime = DateTime.ParseExact(flight.Date + " " + flight.DepartTime, "dd-MM-yyyy HH:mm", null);
            TimeSpan timeDifference = departureDateTime - currentDateTime;

            if (timeDifference.TotalHours >= 1)
            {
                possible_destinations.Add(flight.Destination);

                if (!destinationDates.ContainsKey(flight.Destination))
                {
                    destinationDates[flight.Destination] = new List<string>();
                }

                destinationDates[flight.Destination].Add(flight.Date);
            }
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
            Console.WriteLine("\nPlease choose an option to navigate through the pages of destination:");
            Console.WriteLine("- Enter 'n' to view the next page.");
            Console.WriteLine("- Enter 'b' to view the previous page.");
            Console.WriteLine("- Enter the desired number to jump directly to that page.");
            Console.WriteLine("- Alternatively, you can type the specific destination you're looking for.\n");
            Console.Write(">> ");
            string destinationChoice = Console.ReadLine()!.Trim();

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
                int destinationID;
                if (IsNumber(destinationChoice))
                {
                    destinationID = int.Parse(destinationChoice);
                }
                else
                {
                    destinationID = destinationDictionary.FirstOrDefault(x => x.Value.Equals(destinationChoice, StringComparison.OrdinalIgnoreCase)).Key;
                }

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
                            Console.WriteLine("\nPlease choose an option to navigate through the pages of dates:");
                            Console.WriteLine("- Enter 'n' to view the next page.");
                            Console.WriteLine("- Enter 'b' to view the previous page.");
                            Console.WriteLine("- Enter the desired number to jump directly to that page.");
                            Console.WriteLine("- Alternatively, you can type the specific date you're looking for.\n");
                            Console.Write(">> ");
                            string dateChoice = Console.ReadLine()!.Trim();

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
                                int dateID;
                                if (IsNumber(dateChoice))
                                {
                                    dateID = int.Parse(dateChoice);
                                }
                                else
                                {
                                    dateID = dateDictionary.FirstOrDefault(x => x.Value.Equals(dateChoice, StringComparison.OrdinalIgnoreCase)).Key;
                                }

                                if (dateID >= 1 && dateID <= dateDictionary.Count)
                                {
                                    string selectedDate = dateDictionary[dateID];

                                    List<FlightInfoModel> possible_flights = _flights
                                        .Where(flight => flight.Destination == preferredDestination && flight.Date == selectedDate)
                                        .ToList();

                                    possible_flights.Sort((x, y) => DateTime.ParseExact(x.Date, "dd-MM-yyyy", null).CompareTo(DateTime.ParseExact(y.Date, "dd-MM-yyyy", null)));

                                    PrintFlightTable(possible_flights);

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
        }
    }

    private static bool IsNumber(string input)
    {
        return int.TryParse(input, out _);
    }
    public static void LayoutPlane()
    {
        if (_flights == null) _flights = flightinfoAccess.LoadAll();
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
                string value = taken_seats.Contains(seat) ? "X" : "✓";
                if (row == 3) value = taken_seats.Contains(seat) ? "X" : "i";
                table_row.Add(value);
            }

            Table.AddRow(table_row.ToArray());
        }

        Console.Clear();
        Console.WriteLine("The Layout of the plane: \n");
        SetConsoleColor.WriteEmbeddedColorLine(Table.ToStringAlternative().Replace("X", "[red]X[/red]").Replace("✓", "[green]✓[/green]").Replace("i", "[blue]i[/blue]"));
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
            "Catering",
            "Gate");
            Bookingtable.Options.EnableCount = options.EnableCount;
            foreach (List<string> booking in flight_account_info)
            {
                string cleanString = Regex.Replace(booking[9], @"[^a-zA-Z\s]", "");
                cleanString = cleanString.Replace("(", "").Replace(")", ",");
                Bookingtable.AddRow(Convert.ToInt32(booking[0]) + 1, booking[1], booking[2], booking[3], booking[4], booking[5], booking[6], booking[7], booking[8], cleanString, booking[10]);
            }
            Console.Clear();
            Console.WriteLine("All booked flights: \n");
            Console.WriteLine(Bookingtable.ToString());

            if (delete_flight == false && change_seat == false)
            {
                Console.WriteLine("Do you want to manage your flights? Enter Yes or No");
                string user_input = Console.ReadLine()!;
                if (user_input == "Y" || user_input == "y" || user_input == "Yes" || user_input == "yes")
                {
                    Menu.ManageBookings();
                }
                if (user_input == "N" || user_input == "n" || user_input == "No" || user_input == "no")
                {
                    Menu.Account();
                }
            }
            else if (delete_flight == true)
            {
                if (_flights == null)
                    _flights = flightinfoAccess.LoadAll();

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
                        flightinfoAccess.WriteAll(_flights);

                        // Account section
                        List<List<string>> list_flight_account_info = flight_account_info.ToList();
                        list_flight_account_info.RemoveAll(innerList => innerList.Contains(removed_flight_id.ToString()) && seatList.Any(innerList.Contains));
                        UserLogin.AccountInfo.BookedFlights = list_flight_account_info;
                        accountsAccess.WriteAll(accountList);
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
                    _flights = flightinfoAccess.LoadAll();

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
                bool seatsAvailable = true;

                foreach (List<string> flight in flight_account_info)
                {
                    int ticketPrice = ChangeSeatFlight[0].Price;

                    double TotalPrice = 0.0;


                    if (flight[0] == changed_seat_flightID.ToString() && old_seatsInput == flight[3])
                    {
                        List<string> firstClassSeats = new List<string> { "A1", "A2", "B1", "B2", "C2", "D1", "D2", "E1", "E2", "F1", "F2" };
                        List<string> disabledSeats = new List<string> { "A3", "B3", "C3", "D3", "E3", "F3" };

                        foreach (string new_seat in new_seatList)
                        {

                            if (firstClassSeats.Contains(new_seat)) TotalPrice += (ticketPrice * 2);

                            else TotalPrice += ticketPrice;

                        }

                        foreach (string new_seat in new_seatList)
                        {
                            if (ChangeSeatFlight[0].SeatsTaken.Contains(new_seat))
                            {
                                seatsAvailable = false;
                                break;
                            }
                        }

                        if (!seatsAvailable)
                        {
                            Console.WriteLine("One or more of the new seats are already taken. Please choose different seats.");
                            Console.ReadKey();
                            ViewFlights.SeeBookings(false, true);
                            return;
                        }

                        foreach (string old_seat in old_seatList)
                        {
                            ChangeSeatFlight[0].SeatsTaken.RemoveAll(seat => seat == old_seat);
                        }
                        ChangeSeatFlight[0].SeatsTaken.AddRange(new_seatList);
                        _flights[changed_seat_flightID] = ChangeSeatFlight[0];
                        flightinfoAccess.WriteAll(_flights);

                        // Update account information
                        flight[3] = string.Join(",", new_seatList);
                        flight[8] = TotalPrice.ToString();
                        AccountModel? updatedAccount = accountList.FirstOrDefault(a => a.Id == UserLogin.AccountInfo.Id);
                        updatedAccount!.BookedFlights = flight_account_info;
                        UserLogin.AccountInfo.BookedFlights = flight_account_info;
                        accountsAccess.WriteAll(accountList);
                        seatsChanged = true;
                        break;
                    }
                }

                if (seatsChanged) Console.WriteLine("Seats changed successfully!");

                else Console.WriteLine("No booking found with the specified flight ID and old seat(s).");


            }
        }
        Console.WriteLine("Press any key to go back to the menu.");
        Console.ReadKey();
        Menu.Account();
    }
    public enum CateringOptions
    {
        Drinks,
        Foods
    }
    public static Dictionary<(int, string, CateringOptions), (string[], string[], double)> _Catering = new Dictionary<(int, string, CateringOptions), (string[], string[], double)>();
    public static void Catering()
    {
        Dictionary<(int, string), (string[], string[], double)> DrinkOptions = new Dictionary<(int, string), (string[], string[], double)>
        {
            { (1, "Sky High Spritzer"), (new string[] { "gin", "elderflower liqueur", "lime juice", "soda water" }, new string[] { "nuts", "gluten" }, 10.0 ) },
            { (2, "Aviation Elixir"), (new string[] { "vodka", "blue curaçao", "pineapple juice", "lemonade" }, new string[] { "gluten" }, 5.0) },
            { (3, "Jetsetter Mojito"), (new string[] { "rum", "lime juice", "simple syrup", "mint leaves", "soda water" }, new string[] { "nuts" }, 20.0 ) },
            { (4, "Cloud Nine Cosmopolitan"), (new string[] { "vodka", "triple sec", "cranberry juice", "lime juice" }, new string[] { }, 15.0) },
            { (5, "Inflight Infusion"), (new string[] { "tequila", "orange juice", "grenadine syrup" }, new string[] { "gluten", "shellfish" }, 25.0 ) },
            { (6, "Mile High Margarita"), (new string[] { "tequila", "lime juice", "triple sec", "simple syrup" }, new string[] { "gluten", "dairy" }, 10.0 ) },
            { (7, "Winged Whiskey Sour"), (new string[] { "whiskey", "lemon juice", "simple syrup", "egg white" }, new string[] { "nuts", "dairy", "gluten" }, 15.0 ) },
            { (8, "Turbulence Tonic"), (new string[] { "gin", "tonic water", "lime wedge" }, new string[] { }, 10.0 ) },
            { (9, "First Class Fizz"), (new string[] { "vodka", "orange juice", "club soda", "lemon wedge" }, new string[] { }, 20.0 ) },
            { (10, "Airborne Aperol Spritz"), (new string[] { "aperol", "prosecco", "soda water", "orange slice" }, new string[] { "gluten" }, 30.0) },
            { (11, "Sunset Sipper"), (new string[] { "rum", "pineapple juice", "orange juice", "grenadine syrup" }, new string[] { }, 15.0 ) },
            { (12, "Cabin Cooler"), (new string[] { "gin", "lime juice", "mint leaves", "cucumber", "soda water" }, new string[] { }, 15.0 ) },
            { (13, "Altitude Daiquiri"), (new string[] { "rum", "lime juice", "simple syrup" }, new string[] { }, 10.0 ) },
            { (14, "Sky Blue Sangria"), (new string[] { "white wine", "blueberries", "pineapple chunks", "soda water" }, new string[] { }, 5.0 ) },
            { (15, "Heavenly Hot Chocolate"), (new string[] { "hot chocolate mix", "milk", "whipped cream", "cocoa powder" }, new string[] { "nuts", "dairy" }, 30.0 ) },
            { (16, "Captain's Coffee Blend"), (new string[] { "coffee", "whiskey", "sugar", "cream" }, new string[] { "gluten" }, 30.0 ) }
        };
        Dictionary<(int, string), (string[], string[], double)> FoodOptions = new Dictionary<(int, string), (string[], string[], double)>
        {
            { (1, "Classic Burger"), (new string[] { "beef patty", "cheese", "lettuce", "tomato", "onion", "pickles" }, new string[] { "gluten", "dairy" }, 20.0 ) },
            { (2, "Vegetarian Wrap"), (new string[] { "grilled vegetables", "hummus", "lettuce", "tomato", "cucumber" }, new string[] { "gluten" }, 15.0 ) },
            { (3, "Chicken Caesar Salad"), (new string[] { "grilled chicken", "romaine lettuce", "croutons", "parmesan cheese", "caesar dressing" }, new string[] { "gluten" }, 10.0) },
            { (4, "Pasta Primavera"), (new string[] { "pasta", "mixed vegetables", "olive oil", "garlic", "parmesan cheese" }, new string[] { "gluten", "dairy" }, 15.0) },
            { (5, "Beef Stir-Fry"), (new string[] { "beef strips", "mixed vegetables", "soy sauce", "ginger", "garlic", "rice" }, new string[] { "gluten", "soy" }, 10.0) },
            { (6, "Grilled Salmon"), (new string[] { "salmon fillet", "lemon", "dill", "rice", "steamed vegetables" }, new string[] { }, 15.0) },
            { (7, "Mushroom Risotto"), (new string[] { "arborio rice", "mushrooms", "onion", "garlic", "parmesan cheese" }, new string[] { "gluten", "dairy" }, 10.0) },
            { (8, "Chicken Tikka Masala"), (new string[] { "chicken", "tomato sauce", "cream", "spices", "rice", "naan bread" }, new string[] { "gluten", "dairy" }, 40.0 ) },
            { (9, "Spinach and Feta Stuffed Chicken"), (new string[] { "chicken breast", "spinach", "feta cheese", "garlic", "lemon", "rice", "steamed vegetables" }, new string[] { "gluten", "dairy" }, 5.0 ) },
            { (10, "Vegetable Curry"), (new string[] { "mixed vegetables", "coconut milk", "spices", "rice", "naan bread" }, new string[] { "gluten", "dairy" }, 20.0 ) },
            { (11, "Beef Lasagna"), (new string[] { "beef ragu", "pasta sheets", "bechamel sauce", "mozzarella cheese", "parmesan cheese" }, new string[] { "gluten", "dairy" }, 10.0 ) },
            { (12, "Shrimp Pad Thai"), (new string[] { "shrimp", "rice noodles", "bean sprouts", "scallions", "peanuts", "tamarind sauce" }, new string[] { "gluten", "shellfish", "peanuts" }, 10.0) },
            { (13, "Margherita Pizza"), (new string[] { "pizza dough", "tomato sauce", "mozzarella cheese", "basil" }, new string[] { "gluten", "dairy" }, 25.0 ) },
            { (14, "Fresh Fruit Platter"), (new string[] { "assorted fresh fruits" }, new string[] { }, 10.0 ) },
            { (15, "Cheese and Crackers"), (new string[] { "assorted cheeses", "crackers" }, new string[] { "gluten", "dairy" }, 5.0 ) },
            { (16, "Chocolate Cake"), (new string[] { "chocolate cake", "chocolate ganache", "whipped cream" }, new string[] { "gluten", "dairy" }, 10.0 ) }
        };

        foreach (var drink in DrinkOptions) _Catering[(drink.Key.Item1, drink.Key.Item2, CateringOptions.Drinks)] = (drink.Value.Item1, drink.Value.Item2, drink.Value.Item3);
        foreach (var food in FoodOptions) _Catering[(food.Key.Item1, food.Key.Item2, CateringOptions.Foods)] = (food.Value.Item1, food.Value.Item2, food.Value.Item3);
    }


}