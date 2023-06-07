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
        List<string> main_menu_choices = new List<string>() { " Enter 1 to login", " Enter 2 to create account", " Enter 3 to quit" };
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
                Console.WriteLine("Quitting application");
                System.Environment.Exit(0);
                break;
            case "4":
                ViewFlights.FlightSchedule();
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
        List<string> admin_account_choices = new List<string>() { " Enter 1 to create an account", " Enter 2 to change user password", " Enter 3 to change user name" , " Enter 4 to change user email", " Enter 5 to create a flight", " Enter 6 to view all flights ", " Enter 7 to change banner/logo", " Enter 8 to log out" };

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
                Menu.AdminAccount();
                break;
            case "3":
                admin.ChangeName(true);
                break;
            case "4":
                admin.ChangeEmail(true);
                break;
            case "5":
                Console.WriteLine("Add flight:");
                admin.Add_flight();
                break;
            case "6":
                admin.ViewFlightList();
                break;
            case "7":
                Console.WriteLine("These are all available banners: ");
                AccountFunctionality.PrintBanner(true);
                int banner_choice = Convert.ToInt32(AccountFunctionality.GetInput("Choose a banner, and type the number of the banner you want: "));
                AccountFunctionality.ChangeBanner(banner_choice);
                AdminAccount();
                break;
            case "8":
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

        Console.Write("Select a option: ");
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
                Console.WriteLine("This is not a option!");
                UserEditMenu();
                break;
        }
    }
}