using Newtonsoft.Json;
using ConsoleTables;
public class Admin : User
{
    private static FlightInfoAccess flightinfoAccess = new FlightInfoAccess();

    public void ChangeName() => base.ChangeName(true);
    public void ChangeEmail() => base.ChangeEmail(true);
    public void ChangeUserPassword(string EmailAddress)
    {
        List<AccountModel> account_list = accountsAccess.LoadAll();
        foreach (AccountModel User in account_list)
        {
            if (User.EmailAddress == EmailAddress)
            {
                Console.Write("Please enter a new password\n>> ");
                var Password1 = AccountFunctionality.HidePassword();
                Console.Write("Please enter it again\n>> ");
                var Password2 = AccountFunctionality.HidePassword();
                while (Password1 != Password2)
                {
                    Console.Write("Passwords didn't match. Please enter a new password.\n>> ");
                    Password1 = AccountFunctionality.HidePassword();
                    Console.Write("Please enter it again\n>> ");
                    Password2 = AccountFunctionality.HidePassword();
                }
                User.Password = AccountsLogic.GetHashedSHA256(Password1!);
                accountsAccess.WriteAll(account_list);
                return;
            }
        }
        Console.WriteLine("This Account does either not exist or does not match with given input!\npress Enter to confirm.");
        Console.ReadLine();
        Menu.AdminAccount();

    }
    public void Create_account()
    {
        Console.Clear();
        AccountFunctionality.PrintBanner();
        Console.WriteLine("\n+-------------------------+");
        Console.WriteLine("Choose your option and press enter");
        Console.Write("1. Create User Account\n2. Create Employee Account\n3. Create Admin Account\n");
        Console.Write("+-------------------------+\n>> ");
        int Option = Convert.ToInt32(Console.ReadLine());
        if (Option == 1) UserLogin.MakeAccount(UserLogin.AccountType.User, false);
        else if (Option == 2) UserLogin.MakeAccount(UserLogin.AccountType.Employee, false);
        else if (Option == 3) UserLogin.MakeAccount(UserLogin.AccountType.Admin, false);
        else
        {
            AccountFunctionality.ErrorMessage("This is not an option please choose carefully!");
            Create_account();
        }
    }


    public void Add_flight(string flight_number, string aircraft, string origin, string destination, string date, double flighttime, string departtime, string arrivaltime, int price, string gate)
    {
        List<FlightInfoModel> dataList = flightinfoAccess.LoadAll();
        int highestId = dataList.Max(data => data.FlightID);
        FlightInfoModel newFlight = new FlightInfoModel(highestId + 1, flight_number, aircraft, origin, destination, date, flighttime, departtime, arrivaltime, price, gate);
        dataList.Add(newFlight);
        flightinfoAccess.WriteAll(dataList);
        return;
    }
    public void ModifyFlight()
    {
        if (ViewFlights._flights == null) ViewFlights._flights = flightinfoAccess.LoadAll();

        ViewFlights.FlightSchedule();

        FlightInfoModel flight = ViewFlights._flights.Find(f => f.FlightID == ViewFlights.FlightID - 1)!;

        if (flight == null)
        {
            Console.WriteLine("Flight not found.");
            return;
        }

        Console.WriteLine($"Selected Flight: {flight.FlightNumber} - {flight.Origin} to {flight.Destination}");

        Console.WriteLine("Select the attribute to modify:");
        Console.WriteLine("1. Flight Number");
        Console.WriteLine("2. Aircraft");
        Console.WriteLine("3. Origin");
        Console.WriteLine("4. Destination");
        Console.WriteLine("5. Date");
        Console.WriteLine("6. Flight Time");
        Console.WriteLine("7. Departure Time");
        Console.WriteLine("8. Arrival Time");
        Console.WriteLine("9. Gate");
        Console.WriteLine("10. Price");
        Console.WriteLine("11. WindowMultuplier");
        Console.WriteLine("12. FirstClassMutiplier");

        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine()!;

        Console.Write("Enter the new value: ");
        string newValue = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                flight.FlightNumber = newValue;
                break;
            case "2":
                flight.Aircraft = newValue;
                break;
            case "3":
                flight.Origin = newValue;
                break;
            case "4":
                flight.Destination = newValue;
                break;
            case "5":
                flight.Date = newValue;
                break;
            case "6":
                if (double.TryParse(newValue, out double flightTime))
                {
                    flight.FlightTime = flightTime;
                }
                else
                {
                    Console.WriteLine("Invalid Flight Time.");
                    return;
                }
                break;
            case "7":
                flight.DepartTime = newValue;
                break;
            case "8":
                flight.ArrivalTime = newValue;
                break;
            case "9":
                flight.Gate = newValue;
                break;
            case "10":
                if (int.TryParse(newValue, out int price))
                {
                    flight.Price = price;
                }
                else
                {
                    Console.WriteLine("Invalid Price.");
                    return;
                }
                break;
            case "11":
                ; if (double.TryParse(newValue, out double windowMultiplier))
                {
                    flight.WindowMultiplier = windowMultiplier;
                }
                else
                {
                    Console.WriteLine("Invalid Price.");
                    return;
                }
                break;
            case "12":
                if (double.TryParse(newValue, out double FirstClassMutiplier))
                {
                    flight.FirstClassMultiplier = FirstClassMutiplier;
                }
                else
                {
                    Console.WriteLine("Invalid Price.");
                    return;
                }
                break;
            default:
                Console.WriteLine("Invalid choice.");
                return;
        }

        flightinfoAccess.WriteAll(ViewFlights._flights);

        Console.WriteLine("Flight information updated successfully.");
    }
    public void DeleteFlight()
    {

        if (ViewFlights._flights == null) ViewFlights._flights = flightinfoAccess.LoadAll();

        ViewFlights.FlightSchedule();

        FlightInfoModel flight = ViewFlights._flights.Find(f => f.FlightID == ViewFlights.FlightID - 1)!;

        if (flight == null)
        {
            Console.WriteLine("Flight not found.");
            return;
        }
        Console.WriteLine($"Selected Flight: {flight.FlightNumber} - {flight.Origin} to {flight.Destination}");
        Console.WriteLine("Are you sure you want to delete the flight?");
        string answer = Console.ReadLine()!;

        if (answer == "Y" || answer == "y" || answer == "Yes" || answer == "yes")
        {
            ViewFlights._flights.Remove(flight);
            flightinfoAccess.WriteAll(ViewFlights._flights);

            foreach (var account in ViewFlights.accountList)
            {
                List<List<string>> bookedFlights = account.BookedFlights;

                bookedFlights.RemoveAll(flight => flight.Count > 0 && flight[0] == (ViewFlights.FlightID - 1).ToString());
            }

            accountsAccess.WriteAll(ViewFlights.accountList);

            Console.WriteLine($"You have deleted the flight {flight.FlightNumber} - {flight.Origin} to {flight.Destination}");
            Console.Write("Press Enter to continue...");
            Console.ReadLine();

            Menu.AdminAccount();
        }
        else if (answer == "N" || answer == "n" || answer == "No" || answer == "no")
        {
            Menu.AdminAccount();
        }
        else DeleteFlight();
    }
    public void DeleteUser()
    {
        if (ViewFlights._flights == null)
            ViewFlights._flights = flightinfoAccess.LoadAll();

        ConsoleTable UserTable = new ConsoleTable("ID", "EmailAddress", "Fullname");
        UserTable.Options.EnableCount = ViewFlights.options.EnableCount;

        foreach (var account in ViewFlights.accountList)
        {
            UserTable.AddRow(account.Id, account.EmailAddress, account.FullName);
        }

        Console.Clear();
        Console.WriteLine("All available users:\n");
        Console.WriteLine(UserTable.ToString());

        Console.WriteLine("Enter the ID of the user to delete:");
        string input = Console.ReadLine()!;
        if (!int.TryParse(input, out int userId))
        {
            Console.WriteLine("Invalid input. Please enter a valid user ID.");
            return;
        }

        int userIndex = ViewFlights.accountList.FindIndex(account => account.Id == userId);

        if (userIndex != -1)
        {
            // Remove the user from the account list
            var removedAccount = ViewFlights.accountList[userIndex];
            ViewFlights.accountList.RemoveAt(userIndex);

            // Remove the user's bookings from flights' SeatsTaken list
            foreach (var flightAccountInfo in removedAccount.BookedFlights)
            {
                string flightId = flightAccountInfo[0];
                List<string> seatList = flightAccountInfo[3].Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

                var flight = ViewFlights._flights.FirstOrDefault(f => f.FlightID.ToString() == flightId);
                if (flight != null)
                {
                    foreach (var seat in seatList)
                    {
                        flight.SeatsTaken.Remove(seat);
                    }
                }
            }

            // Write the updated account list to the data store
            accountsAccess.WriteAll(ViewFlights.accountList);

            // Write the updated flights to the data store
            flightinfoAccess.WriteAll(ViewFlights._flights);

            // Display success message or perform additional actions if needed
            Console.WriteLine("User deleted successfully.");
        }
        else
        {
            // User not found in the account list
            Console.WriteLine("User not found.");
        }
    }
    public void ViewFlightList()
    {
        FlightInfoAccess flightinfoAccess = new FlightInfoAccess();
        ViewFlights._flights = flightinfoAccess.LoadAll();
        ViewFlights.FlightMenu();
    }
}