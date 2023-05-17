static class Menu
{
    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role

    // why is this int public? it's only used in PrintBanner()
    static private int banner_number = 1;
    static public void PrintBanner(bool print_all_options = false)
    {
        List<string> banner_options = new List<string>();
        banner_options.Add(" _____       _   _               _                            _      _ _  \n|  __ \\     | | | |             | |                     /\\   (_)    | (_) \n| |__) |___ | |_| |_ ___ _ __ __| | __ _ _ __ ___      /  \\   _ _ __| |_ _ __   ___  ___ \n|  _  // _ \\| __| __/ _ \\ '__/ _` |/ _` | '_ ` _ \\    / /\\ \\ | | '__| | | '_ \\ / _ \\/ __|\n| | \\ \\ (_) | |_| ||  __/ | | (_| | (_| | | | | | |  / ____ \\| | |  | | | | | |  __/\\_  \\\n|_|  \\_\\___/ \\__|\\__\\___|_|  \\__,_|\\__,_|_| |_| |_| /_/    \\_\\_|_|  |_|_|_| |_|\\___||___/");
        banner_options.Add(" ____   __  ____  ____  ____  ____  ____   __   _  _     __   __  ____  __    __  __ _  ____  ____ \r\n(  _ \\ /  \\(_  _)(_  _)(  __)(  _ \\(    \\ / _\\ ( \\/ )   / _\\ (  )(  _ \\(  )  (  )(  ( \\(  __)/ ___)\r\n )   /(  O ) )(    )(   ) _)  )   / ) D (/    \\/ \\/ \\  /    \\ )(  )   // (_/\\ )( /    / ) _) \\___ \\\r\n(__\\_) \\__/ (__)  (__) (____)(__\\_)(____/\\_/\\_/\\_)(_/  \\_/\\_/(__)(__\\_)\\____/(__)\\_)__)(____)(____/");
        banner_options.Add("   __       _   _               _                     _   _      _ _                 \r\n  /__\\ ___ | |_| |_ ___ _ __ __| | __ _ _ __ ___     /_\\ (_)_ __| (_)_ __   ___  ___ \r\n / \\/// _ \\| __| __/ _ \\ '__/ _` |/ _` | '_ ` _ \\   //_\\\\| | '__| | | '_ \\ / _ \\/ __|\r\n/ _  \\ (_) | |_| ||  __/ | | (_| | (_| | | | | | | /  _  \\ | |  | | | | | |  __/\\__ \\\r\n\\/ \\_/\\___/ \\__|\\__\\___|_|  \\__,_|\\__,_|_| |_| |_| \\_/ \\_/_|_|  |_|_|_| |_|\\___||___/");
        banner_options.Add("    ____        __  __                __                   ___    _      ___                \r\n   / __ \\____  / /_/ /____  _________/ /___ _____ ___     /   |  (_)____/ (_)___  ___  _____\r\n  / /_/ / __ \\/ __/ __/ _ \\/ ___/ __  / __ `/ __ `__ \\   / /| | / / ___/ / / __ \\/ _ \\/ ___/\r\n / _, _/ /_/ / /_/ /_/  __/ /  / /_/ / /_/ / / / / / /  / ___ |/ / /  / / / / / /  __(__  ) \r\n/_/ |_|\\____/\\__/\\__/\\___/_/   \\__,_/\\__,_/_/ /_/ /_/  /_/  |_/_/_/  /_/_/_/ /_/\\___/____/");

        if (print_all_options)
        {
            for (int i = 0; i < banner_options.Count(); i++)
            {
                Console.WriteLine($"Banner {i + 1}:\n{banner_options[i]}\n\n");
            }

            Console.WriteLine("\nCurrent Banner: ");
        }
        Console.WriteLine(banner_options[banner_number - 1]);
    }


    static public void Start()
    {
        Console.Clear();
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
        Console.WriteLine("\nACCOUNT MENU:");
        List<string> main_account_choices = new List<string>() { " Enter 1 to book a flight", " Enter 2 to see bookings", " Enter 3 to log out" };
        Console.WriteLine("\n+-------------------------+");
        foreach (string item in main_account_choices) Console.WriteLine(item);

        // we need a function to write those lines probably
        Console.WriteLine("+-------------------------+");
        string input = AccountFunctionality.GetInput();

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
        Console.WriteLine("\nADMIN MENU:");

        Admin admin = new Admin();

        // edit/delete flights should be in a submenu in 3, but for now this name is more accurate
        List<string> admin_account_choices = new List<string>() { " Enter 1 to create an account", " Enter 2 to change user password", " Enter 3 to create a flight", " Enter 4 to change banner/logo", " Enter 5 to log out" };

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
                Console.WriteLine("Add flight:");
                admin.Add_flight();
                break;
            case "4":
                Console.WriteLine("These are all available banners: ");
                PrintBanner(true);
                int banner_choice = Convert.ToInt32(AccountFunctionality.GetInput("Choose a banner, and type the number of the banner you want: "));
                banner_number = banner_choice;
                PrintBanner();
                AdminAccount();
                break;
            case "5":
                Menu.Start();
                break;
            default:
                AccountFunctionality.ErrorMessage("Invalid input, please try again, press Enter to confirm.");
                AdminAccount();
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
                Console.WriteLine("Invalid input, please try again");
                Account();
                break;
        }
    }
}