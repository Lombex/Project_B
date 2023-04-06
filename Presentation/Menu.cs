static class Menu
{
    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void PrintBanner()
    {
        Console.WriteLine(" _____       _   _               _                            _      _ _  ");
        Console.WriteLine("|  __ \\     | | | |             | |                     /\\   (_)    | (_) ");
        Console.WriteLine("| |__) |___ | |_| |_ ___ _ __ __| | __ _ _ __ ___      /  \\   _ _ __| |_ _ __   ___  ___ ");
        Console.WriteLine("|  _  // _ \\| __| __/ _ \\ '__/ _` |/ _` | '_ ` _ \\    / /\\ \\ | | '__| | | '_ \\ / _ \\/ __|");
        Console.WriteLine("| | \\ \\ (_) | |_| ||  __/ | | (_| | (_| | | | | | |  / ____ \\| | |  | | | | | |  __/\\_  \\");
        Console.WriteLine("|_|  \\_\\___/ \\__|\\__\\___|_|  \\__,_|\\__,_|_| |_| |_| /_/    \\_\\_|_|  |_|_|_| |_|\\___||___/");
    }
    static public void Start()
    {
        PrintBanner();
        Console.WriteLine("\n!! In this application, please press the 'Enter' button every time you want to confirm !!");
        List<string> main_menu_choices = new List<string>() { " Enter 1 to login", " Enter 2 to create account", " Enter 3 to quit" };
        Console.WriteLine("\n+-------------------------+");
        foreach (string item in main_menu_choices)
        {
            Console.WriteLine(item);
        }
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
                UserLogin.MakeAccount(false);
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
        List<string> main_account_choices = new List<string>() { " Enter 1 to book a flight", " Enter 2 to see bookings", " Enter 3 to log out" };
        Console.WriteLine("\n+-------------------------+");
        foreach (string item in main_account_choices)
        {
            Console.WriteLine(item);
        }
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

        Admin admin = new Admin();  

        List<string> admin_account_choices = new List<string>() { " Enter 1 to create user account", " Enter 2 to check user password", " Enter 3 to change user password", " Enter 4 to log out" };
        Console.WriteLine("\n+-------------------------+");
        foreach (string item in admin_account_choices)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("+-------------------------+");
        Console.Write(">> ");
        string input = Console.ReadLine()!;
        switch (input)
        {
            case "1":
                admin.create_account();
                break;
            case "2":
                Console.Write("Please enter an email\n>>");
                var email_address = Console.ReadLine();
                Console.WriteLine("Please enter an password\n>>");
                var password = Console.ReadLine();
                admin.ChangeUserPassword(email_address, password);
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