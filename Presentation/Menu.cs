static class Menu
{

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    static public void Start()
    {
        List<string> main_menu_choises = new List<string>() {"Enter 1 to login", "Enter 2 to create accaunt", "Enter 3 to quit"};
        foreach(string item in main_menu_choises)
        {
            Console.WriteLine(item);
        }
        string input = Console.ReadLine();

        if (input == "1")
        {
            UserLogin.Start();
        }
        else if (input == "2")
        {
            Console.WriteLine("Welcome to 'make account'");
            UserLogin.MakeAccount();
        }
        else if (input == "3")
        {
            Console.WriteLine("Quitting application");
        }
        else
        {
            Console.WriteLine("Invalid input, please try again");
            Menu.Start();
        }

    }

    static public void Account()
    {
        List<string> main_account_choises = new List<string>() {"Enter 1 to book a flight", "Enter 2 to see bookings", "Enter 3 to log out"};
        foreach(string item in main_account_choises)
        {
            Console.WriteLine(item);
        }
        string input = Console.ReadLine();

        if (input == "1")
        {
           ViewFlights.Menu();
        }
        else if (input == "2")
        {
            AccountFunctionaliy.Menu();
        }
        else if (input == "3")
        {
            Menu.Start();
        }
        else
        {
            Console.WriteLine("Invalid input, please try again");
            Menu.Account();
        }
        
    }
}