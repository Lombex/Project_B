static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        List<string> main_menu_choises = new List<string>() { "Enter 1 to login", "Enter 2 to create account", "Enter 3 to quit" };
        foreach (string item in main_menu_choises)
        {
            Console.WriteLine(item);
        }
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                UserLogin.Start();
                break;
            case "2":
                Console.WriteLine("Welcome to 'make account'");
                UserLogin.MakeAccount();
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
        List<string> main_account_choises = new List<string>() { "Enter 1 to book a flight", "Enter 2 to see bookings", "Enter 3 to log out" };
        foreach (string item in main_account_choises)
        {
            Console.WriteLine(item);
        }
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                ViewFlights.Menu();
                break;
            case "2":
                AccountFunctionaliy.Menu();
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
}