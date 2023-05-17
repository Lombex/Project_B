using Newtonsoft.Json;
public static class Admin : Employee
{

    // Create new method edit customer account data

    // Edit, remove, add booking

    // Create new method add and remove flights

    // Create new method change flight data

    public override void ChangeUserPassword(string EmailAddress)
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
            Console.WriteLine("This is not an option please choose carefully!");
            Create_account();
        }
    }
    public void Add_flight()
    {
        Console.Write("Enter flight number\n>> ");
        string flight_number = Console.ReadLine()!;
        Console.Write("Enter Aircraft\n>> ");
        string aircraft = Console.ReadLine()!;
        Console.Write("Enter origin\n>> ");
        string origin = Console.ReadLine()!;
        Console.Write("Enter destination\n>> ");
        string destination = Console.ReadLine()!;
        Console.Write("Enter Date\n>> ");
        string date = Console.ReadLine()!;
        Console.Write("Enter flighttime (in hours) [ format: 0.1 ]\n>> ");
        double flighttime = Convert.ToDouble(Console.ReadLine());
        Console.Write("Enter depart time (local)\n>> ");
        string departtime = Console.ReadLine()!;
        Console.Write("Enter arrival time (local)\n>> ");
        string arrivaltime = Console.ReadLine()!;
        Console.Write("Enter gate\n>> ");
        string gate = Console.ReadLine()!;

        List<FlightInfoModel> dataList = FlightInfoAccess.LoadAll();
        FlightInfoModel newFlight = new FlightInfoModel(flight_number, aircraft, origin, destination, date, flighttime, departtime, arrivaltime, gate);
        dataList.Add(newFlight);
        FlightInfoAccess.WriteAll(dataList);

        Menu.AdminAccount();
    }
    public void ViewFlightList() => ViewFlights.Menu();
}