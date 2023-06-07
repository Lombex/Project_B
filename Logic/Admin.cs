using Newtonsoft.Json;
public class Admin : User
{

    // Create new method edit customer account data

    // Edit, remove, add booking

    // Create new method add and remove flights

    // Create new method change flight data

    public void ChangeUserPassword(string EmailAddress)
    {
        List<AccountModel> account_list = AccountsAccess.LoadAll();
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
                AccountsAccess.WriteAll(account_list);
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
    public void Add_flight()
    {
        string flight_number = AccountFunctionality.GetInput("Enter flight number")!;
        string aircraft = AccountFunctionality.GetInput("Enter Aircraft");
        string origin = AccountFunctionality.GetInput("Enter origin");
        string destination = AccountFunctionality.GetInput("Enter destination");
        string date = AccountFunctionality.GetInput("Enter Date");
        double flighttime = Convert.ToDouble(AccountFunctionality.GetInput("Enter flighttime (in hours) [ format: 0.1 ]"));
        string departtime = AccountFunctionality.GetInput("Enter depart time (local)");
        string arrivaltime = AccountFunctionality.GetInput("Enter arrival time (local)");
        string gate = AccountFunctionality.GetInput("Enter gate")!;
        int price = Convert.ToInt32(AccountFunctionality.GetInput("Enter standard price of the flight"));

        List<FlightInfoModel> dataList = FlightInfoAccess.LoadAll();
        int highestId = dataList.Max(data => data.FlightID);
        FlightInfoModel newFlight = new FlightInfoModel(highestId + 1, flight_number, aircraft, origin, destination, date, flighttime, departtime, arrivaltime, price, gate);
        dataList.Add(newFlight);
        FlightInfoAccess.WriteAll(dataList);

        Menu.AdminAccount();
    }
    public void ViewFlightList() => ViewFlights.FlightMenu();
}