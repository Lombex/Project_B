static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine();
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            Console.WriteLine("Welcome back " + acc.FullName);
            Console.WriteLine("Your email number is " + acc.EmailAddress);
            Menu.Account();

            //Write some code to go back to the menu
            //Menu.Start();
        }
        else
        {
            Console.WriteLine("No account found with that email and password");
            Menu.Start();
        }
    }

    public static void MakeAccount()
    {
        Console.WriteLine("Please enter your full name");
        string full_name = Console.ReadLine();
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        // Check if email is already in json! 
        Console.WriteLine("Please enter your password");
        string password_1 = Console.ReadLine();
        Console.WriteLine("Please enter your again password");
        string password_2 = Console.ReadLine();

        if (password_1 != password_2)
        {
            Console.WriteLine("Password is not the same, please try again..");
            UserLogin.MakeAccount();
        }

        // Make function in logic layer that adds this account to json 

    }
}