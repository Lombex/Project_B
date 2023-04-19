using Newtonsoft.Json;
public class Admin : User
{

    // Create new method edit customer account data

    // Edit, remove, add booking 

    // Create new method add and remove flights

    // Create new method change flight data

    public void ChangeUserPassword(string EmailAddress, string ChangedPassword)
    {
        foreach (AccountModel User in AccountsAccess.LoadAll())
        {
            if (User.EmailAddress == EmailAddress)
            {
                User.Password = AccountsLogic.GetHashedSHA256(ChangedPassword);
                Menu.AdminAccount();
            }
            else
            {
                Console.WriteLine("This Account does either not exists or does not match with given input!");
                Menu.AdminAccount();
            }
        }
    }
    public void Create_account()
    {
        Console.Clear();
        Menu.PrintBanner();
        Console.WriteLine("\n+-------------------------+");
        Console.WriteLine("Choose your option and press enter");
        Console.Write("1. Create User Account\n2. Create Employee Account\n>> ");
        Console.WriteLine("+-------------------------+");
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
        Console.WriteLine("Enter flight number\n>>");
        string flight_number = Console.ReadLine()!;
        Console.WriteLine("Enter Aircraft\n>>");
        string aircraft = Console.ReadLine()!;
        Console.WriteLine("Enter origin\n>>");
        string origin = Console.ReadLine()!;
        Console.WriteLine("Enter destination\n>>");
        string destination = Console.ReadLine()!;
        Console.WriteLine("Enter Date\n>>");
        string date = Console.ReadLine()!;
        Console.WriteLine("Enter flighttime (in hours)\n>>");
        double flighttime = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine("Enter depart time (local)\n>>");
        string departtime = Console.ReadLine()!;
        Console.WriteLine("Enter arrival time (local)\n>>");
        string arrivaltime = Console.ReadLine()!;
        Console.WriteLine("Enter gate\n>>");
        string gate = Console.ReadLine()!;

        List<FlightInfoModel> dataList = FlightInfoAccess.LoadAll();
        FlightInfoModel newFlight = new FlightInfoModel(flight_number, aircraft, origin, destination, date, flighttime, departtime, arrivaltime, gate);
        dataList.Add(newFlight);
        FlightInfoAccess.WriteAll(dataList);

        Menu.AdminAccount();
    }
}