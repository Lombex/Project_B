static class Menu
{
    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public int banner_number = 1;
    static public void PrintBanner(bool print_all_options = false)
    {
        List<string> banner_options = new List<string>();
        banner_options.Add(" _____       _   _               _                            _      _ _  \n|  __ \\     | | | |             | |                     /\\   (_)    | (_) \n| |__) |___ | |_| |_ ___ _ __ __| | __ _ _ __ ___      /  \\   _ _ __| |_ _ __   ___  ___ \n|  _  // _ \\| __| __/ _ \\ '__/ _` |/ _` | '_ ` _ \\    / /\\ \\ | | '__| | | '_ \\ / _ \\/ __|\n| | \\ \\ (_) | |_| ||  __/ | | (_| | (_| | | | | | |  / ____ \\| | |  | | | | | |  __/\\_  \\\n|_|  \\_\\___/ \\__|\\__\\___|_|  \\__,_|\\__,_|_| |_| |_| /_/    \\_\\_|_|  |_|_|_| |_|\\___||___/\n");
        banner_options.Add("TEST banner 2");
        banner_options.Add("TEST banner 3");
        banner_options.Add("TEST banner 4");

        if (print_all_options)
        {
            foreach(string banner in banner_options)
            {
                Console.WriteLine(banner);
            }

            Console.WriteLine("\n\nCurrent Banner: ");
        }

        switch (banner_number)
        {
            case 1:
                Console.WriteLine(banner_options[0]);
                break;
            case 2:
                Console.WriteLine(banner_options[1]);
                break;
            case 3: 
                Console.WriteLine(banner_options[2]);
                break;
            case 4:
                Console.WriteLine(banner_options[3]);
                break;
        }
        
    }
    static public void Start()
    {
        PrintBanner();
        Console.WriteLine("\n!! In this application, please press the 'Enter' button every time you want to confirm !!");
        List<string> main_menu_choices = new List<string>() { " Enter 1 to login", " Enter 2 to create account", " Enter 3 to quit" };
        Console.WriteLine("\n+-------------------------+");
        foreach (string item in main_menu_choices) Console.WriteLine(item);
        Console.WriteLine("+-------------------------+");
        Console.Write(">> ");
        string input = Console.ReadLine()!;

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
                break;
            default:
                Console.WriteLine("Invalid input, please try again");
                Start();
                break;
        }
    }
    static public void Account()
    {
        Console.Clear();
        PrintBanner();
        Console.WriteLine("ACCOUNT MENU:");
        List<string> main_account_choices = new List<string>() {" Enter 1 to book a flight", " Enter 2 to see bookings", " Enter 3 to log out" };
        Console.WriteLine("\n+-------------------------+");
        foreach (string item in main_account_choices) Console.WriteLine(item);

        Console.WriteLine("+-------------------------+");
        Console.Write(">> ");
        string input = Console.ReadLine()!;

        switch (input)
        {
            case "1":
                ViewFlights.Menu();
                break;
            case "2":
                AccountFunctionality.Menu();
                break;
            case "3":
                Start();
                break;
            default:
                Console.WriteLine("Invalid input, please try again");
                Account();
                break;
        }
    }
    public static void AdminAccount()
    {
        Console.Clear();
        PrintBanner();
        Console.WriteLine("ADMIN MENU:");

        Admin admin = new Admin();

        List<string> admin_account_choices = new List<string>() { " Enter 1 to create user/employee account", " Enter 2 to change user password"," Enter 3 for flight management", " Enter 4 to change banner/logo", " Enter 5 to log out" };
        Console.WriteLine("\n+-------------------------+");
        foreach (string item in admin_account_choices) Console.WriteLine(item);

        Console.WriteLine("+-------------------------+");
        Console.Write(">> ");
        string input = Console.ReadLine()!;
        switch (input)
        {
            case "1":
                admin.Create_account();
                break;
            case "2":
                Console.Write("Please enter an email\n>>");
                var email_address = Console.ReadLine();
                Console.WriteLine("Please enter an password\n>>");
                var password = Console.ReadLine();
                admin.ChangeUserPassword(email_address!, password!);
                break;
            case "3":
                Console.WriteLine("Add flight:");
                admin.Add_flight();
                break;
            case "4":
                Console.WriteLine("This are all available banners: ");
                PrintBanner(true);
                Console.WriteLine("Choose a banner, type the number of the banner you want: \n>>");
                int banner_choise = Convert.ToInt32(Console.ReadLine());
                banner_number = banner_choise;
                PrintBanner();
                AdminAccount();
                break;
            case "5":
                Menu.Start();
                break;
            default:
                Console.WriteLine("Invalid input, please try again");
                Account();
                break;
        }
    }
    public static void EmployeeAccount()
    {
        Console.Clear();
        PrintBanner();
        Console.WriteLine("EMPLOYEE MENU:");

        // Employee employee = new Employee();

        List<string> employee_account_choices = new List<string>() { " Enter 1 to create user account", " Enter 2 to change user password", " Enter 3 to log out" };
        Console.WriteLine("\n+-------------------------+");
        foreach (string item in employee_account_choices) Console.WriteLine(item);

        Console.WriteLine("+-------------------------+");
        Console.Write(">> ");
        string input = Console.ReadLine()!;
        switch (input)
        {
            case "1":
                // employee.Create_account();
                break;
            case "2":
                Console.Write("Please enter an email\n>>");
                var email_address = Console.ReadLine();
                Console.WriteLine("Please enter an password\n>>");
                var password = Console.ReadLine();
                // employee.ChangeUserPassword(email_address!, password!);
                break;
            case "3":
                Menu.Start();
                break;
            default:
                Console.WriteLine("Invalid input, please try again");
                Account();
                break;
        }
    }
}