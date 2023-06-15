using ConsoleTables;
using Project.Presentation;

static class Menu
{
    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        Console.Clear();
        AccountFunctionality.PrintBanner();
        Console.WriteLine("\n!! In this application, please press the 'Enter' button every time you want to confirm !!");
        List<string> main_menu_choices = new List<string>() { " Enter 1 to login", " Enter 2 to create account", " Enter 3 for airport info", " Enter 4 to quit" };
        Console.WriteLine("\n+-------------------------+");
        foreach (string item in main_menu_choices) Console.WriteLine(item);
        Console.WriteLine("+-------------------------+");
        string input = AccountFunctionality.GetInput();

        switch (input)
        {
            case "1":
                UserLogin.Start();
                break;
            case "2":
                Console.WriteLine("Welcome to 'create account'");
                UserLogin.MakeAccount(UserLogin.AccountType.User, true);
                break;
            case "3":
                PrintAirportInformation();
                Start();
                break;
            case "4":
                Console.WriteLine("Quitting application");
                System.Environment.Exit(0);
                break;
            default:
                AccountFunctionality.ErrorMessage();
                Start();
                break;
        }
    }
    static public void Account()
    {
        Console.Clear();
        AccountFunctionality.PrintBanner();
        Console.WriteLine("\nACCOUNT MENU:");
        List<string> main_account_choices = new List<string>() { " Enter 1 to book a flight", " Enter 2 to see bookings", " Enter 3 to change account info", " Enter 4 to log out" };
        Console.WriteLine("\n+-------------------------+");
        foreach (string item in main_account_choices) Console.WriteLine(item);

        // we need a function to write those lines probably
        Console.WriteLine("+-------------------------+");
        string input = AccountFunctionality.GetInput();

        switch (input)
        {
            case "1":
                ViewFlights.FlightMenu();
                break;
            case "2":
                ViewFlights.SeeBookings(false, false);
                break;
            case "3":
                UserEditMenu();
                break;
            case "4":
                Start();
                break;
            default:
                AccountFunctionality.ErrorMessage();
                Menu.Account();
                break;
        }
    }
    public static void AdminAccount()
    {
        Console.Clear();
        AccountFunctionality.PrintBanner();
        Console.WriteLine("\nADMIN MENU:");

        Admin admin = new Admin();

        // edit/delete flights should be in a submenu in 3, but for now this name is more accurate
        List<string> admin_account_choices = new List<string>() { " Enter 1 to create an account", " Enter 2 to change user password", " Enter 3 to change user name", " Enter 4 to change user email", " Enter 5 to delete a user", " Enter 6 to create a flight", " Enter 7 to modify a flight", " Enter 8 to view all flights ", " Enter 9 to delete flight", " Enter 10 to change banner/logo", " Enter 11 to log out" };

        Console.WriteLine("\n+-------------------------+");
        foreach (string item in admin_account_choices) Console.WriteLine(item);

        Console.WriteLine("+-------------------------+");
        string input = AccountFunctionality.GetInput();
        switch (input)
        {
            case "1":
                admin.Create_account();
                break;
            case "2":
                var email_address = AccountFunctionality.GetInput("Please enter an email");
                admin.ChangeUserPassword(email_address!);
                AdminAccount();
                break;
            case "3":
                admin.ChangeName(true);
                AdminAccount();
                break;
            case "4":
                admin.ChangeEmail(true);
                AdminAccount();
                break;
            case "5":
                admin.DeleteUser();
                AdminAccount();
                break;
            case "6":
                Console.WriteLine("Add flight:");
                admin.Add_flight();
                AdminAccount();
                break;
            case "7":
                admin.ModifyFlight();
                AdminAccount();
                break;
            case "8":
                admin.ViewFlightList();
                AdminAccount();
                break;
            case "9":
                admin.DeleteFlight();
                break;
            case "10":
                Console.WriteLine("These are all available banners: ");
                AccountFunctionality.PrintBanner(true);
                int banner_choice = Convert.ToInt32(AccountFunctionality.GetInput("Choose a banner, and type the number of the banner you want: "));
                AccountFunctionality.ChangeBanner(banner_choice);
                AdminAccount();
                break;
            case "11":
                Menu.Start();
                break;
            default:
                AccountFunctionality.ErrorMessage();
                AdminAccount();
                break;
        }
    }
    public static void EmployeeAccount()
    {
        Console.Clear();
        AccountFunctionality.PrintBanner();
        Console.WriteLine("EMPLOYEE MENU:");

        // Employee employee = new Employee();

        List<string> employee_account_choices = new List<string>() { " Enter 1 to create user account", " Enter 2 to change user password", " Enter 3 to log out" };
        Console.WriteLine("\n+-------------------------+");
        foreach (string item in employee_account_choices) Console.WriteLine(item);

        Console.WriteLine("+-------------------------+");
        string input = AccountFunctionality.GetInput();
        switch (input)
        {
            case "1":
                // employee.Create_account();
                break;
            case "2":
                var email_address = AccountFunctionality.GetInput("Please enter an email");
                // vv  this isn't how we should be doing this  vv
                var password = AccountFunctionality.GetInput("Please enter a password");

                // vv why is this commented out?
                // employee.ChangeUserPassword(email_address!, password!);
                break;
            case "3":
                Menu.Start();
                break;
            default:
                AccountFunctionality.ErrorMessage();
                Account();
                break;
        }
    }
    public static void ManageBookings()
    {
        Console.Clear();
        AccountFunctionality.PrintBanner();
        Console.WriteLine("\nManage Bookings Menu");
        List<string> manage_booking_choices = new List<string>() { " Enter 1 to delete a booking", " Enter 2 to change your seat", " Enter 3 to go back" };

        Console.WriteLine("\n+-------------------------+");
        foreach (string item in manage_booking_choices) Console.WriteLine(item);

        Console.WriteLine("+-------------------------+");
        string input = AccountFunctionality.GetInput();
        switch (input)
        {
            case "1":
                ViewFlights.SeeBookings(true, false);
                break;
            case "2":
                ViewFlights.SeeBookings(false, true);
                break;
            case "3":
                Menu.Account();
                break;
            default:
                AccountFunctionality.ErrorMessage();
                Menu.ManageBookings();
                break;
        }
    }
    public static void UserEditMenu()
    {

        List<string> Options = new List<string> { "Enter 1 to change name", "Enter 2 to change password", "Enter 3 to change email", "Enter 4 to go back" };
        Console.WriteLine("\n+-------------------------+");
        foreach (string option in Options) Console.WriteLine(option);
        Console.WriteLine("+-------------------------+");

        Console.Write("\n>> ");
        string SelectedOption = Console.ReadLine()!;
        User user = new User();

        switch (SelectedOption)
        {
            case "1":
                user.ChangeName();
                Account();
                break;
            case "2":
                user.ChangePassword();
                Account();
                break;
            case "3":
                user.ChangeEmail();
                Account();
                break;
            case "4":
                Account();
                break;
            default:
                Console.WriteLine("This is not an option!");
                UserEditMenu();
                break;
        }
    }
    public static void PrintAirportInformation()
    {
        Console.Clear();
        AccountFunctionality.PrintBanner();
        Console.WriteLine("Airport information: \n\nAddres: Driemanssteeweg 107, 3011 WN in Rotterdam\n\nNow that Rotterdam South is becoming more and more important for the city, \nthe wish has arisen that it should also be possible to fly from Rotterdam South.\nJake Darcy has started an airport where sustainability is paramount. \nWe started small, initially only flying within Europe. We will be using our own planes and we set up our own airline: Rotterdam Airlines. \nRotterdam Airlines has initially 1 aircraft at its disposal. A Boeing 737.\nWe do like to welcome you on board of Rotterdam Airlines! ");
        Console.WriteLine("\nPlease press enter to go back!");
        Console.ReadLine();
    }


    public static List<(string, double)> CateringOrders = new List<(string, double)>();
    public static double GetTotalOrderAmount(List<(string, double)> Order)
    {
        double TotalAmount = 0;
        foreach (var order in Order) TotalAmount += order.Item2;
        return TotalAmount;
    }
    public static void CateringMenu()
    {
        ViewFlights.Catering();
        Console.WriteLine("Please select your option: ");
        string[] Options = { "1. Drinks", "2. Food", "3. Exit" };
        foreach (string option in Options) Console.WriteLine(option);
        string Selection = Console.ReadLine()!;
        switch (Selection.ToLower())
        {
            case "1" or "drinks":
                ConsoleTable DrinksTable = new ConsoleTable("ID", "Drink", "Price", "Ingredients", "Allergies");
                DrinksTable.Options.EnableCount = ViewFlights.options.EnableCount;
                foreach (var Drinks in ViewFlights._Catering)
                    if (Drinks.Key.Item3 == ViewFlights.CateringOptions.Drinks) DrinksTable.AddRow(Drinks.Key.Item1, Drinks.Key.Item2, "$ " + Drinks.Value.Item3, string.Join(", ", Drinks.Value.Item1), string.Join(", ", Drinks.Value.Item2)); 
                Console.Clear();
                Console.WriteLine(DrinksTable.ToString());
                Console.WriteLine("\nSelect a drink using the ID: ");
                
                try
                {
                    int DrinkSelection = Convert.ToInt32(Console.ReadLine()!);
                    foreach (var Items in ViewFlights._Catering) 
                        if (Items.Key.Item1.Equals(DrinkSelection) && Items.Key.Item3 == ViewFlights.CateringOptions.Drinks)
                        {
                            Console.WriteLine("Drink has been added to your order!");
                            CateringOrders.Add((Items.Key.Item2, Items.Value.Item3));
                        }
                    CateringMenu();
                    break;
                }
                catch (Exception) { }
                
                break;
            case "2" or "food":
                ConsoleTable FoodTable = new ConsoleTable("ID", "Food", "Price", "Ingredients", "Allergies");
                FoodTable.Options.EnableCount = ViewFlights.options.EnableCount;
                foreach (var Food in ViewFlights._Catering)
                    if (Food.Key.Item3 == ViewFlights.CateringOptions.Foods) FoodTable.AddRow(Food.Key.Item1, Food.Key.Item2, "$ " + Food.Value.Item3, string.Join(", ", Food.Value.Item1), string.Join(", ", Food.Value.Item2));
                Console.Clear();
                Console.WriteLine(FoodTable.ToString());
                Console.WriteLine("\nSelect food using the ID: ");

                try
                {
                    int FoodSelection = Convert.ToInt32(Console.ReadLine()!);
                    foreach (var Items in ViewFlights._Catering)
                        if (Items.Key.Item1.Equals(FoodSelection) && Items.Key.Item3 == ViewFlights.CateringOptions.Foods)
                        {
                            Console.WriteLine("Food has been added to your order!");
                            CateringOrders.Add((Items.Key.Item2, Items.Value.Item3));
                        }
                    CateringMenu();
                    break;
                }
                catch (Exception) { }

                break;
            case "3" or "exit":
                break;
            default:
                break;
        }
    }
}