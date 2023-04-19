static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();
    public static void Start()
    {
        Console.Write("Welcome to the login page\n");
        Console.Write("Please enter your email address\n>> ");
        string email = Console.ReadLine()!;
        Console.Write("Please enter your password\n>> ");
        string? password = AccountsLogic.GetHashedSHA256(AccountFunctionality.HidePassword());
        AccountModel? acc = accountsLogic.CheckLogin(email, password);
        if (acc == null)
        {
            Console.Clear();
            Console.WriteLine("No account found with that email and password (Press enter to continue)");
            Menu.Start();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Welcome back " + acc.FullName);
            Console.WriteLine("Your email address is " + acc.EmailAddress);
            if (acc.IsAdmin) Menu.AdminAccount();
            else Menu.Account();
        }
    }

    public enum AccountType
    {
        User,
        Employee,
        Admin
    }
    public static void MakeAccount(AccountType type, bool back_to_menu)
    {
        Console.Write("Please enter your full name\n>> ");
        string full_name = Console.ReadLine()!;
        Console.Write("Please enter your email address\n>> ");
        string email = Console.ReadLine()!;
        Console.Write("Please enter your password\n>> ");
        string password_1 = AccountsLogic.GetHashedSHA256(AccountFunctionality.HidePassword());
        Console.Write("Please enter your password again\n>> ");
        string password_2 = AccountsLogic.GetHashedSHA256(AccountFunctionality.HidePassword());

        if (password_1 != password_2)
        {
            Console.WriteLine("Password is not the same, please try again..");
            UserLogin.MakeAccount(type, back_to_menu);
        }
        else
        {
            List<AccountModel> dataList = AccountsAccess.LoadAll();
            if (dataList.Any(data => data.EmailAddress == email))
                Console.WriteLine("An account with the same email address already exists. Please choose a different email address.");
            else
            {
                int highestId = dataList.Max(data => data.Id);
                AccountModel newData = new AccountModel(highestId + 1, email, password_1, full_name); // admin is automatically false
                dataList.Add(newData);
                AccountsAccess.WriteAll(dataList);
            }
            if (back_to_menu == false)
            {
                Menu.AdminAccount();
            }
            
            switch (type)
            {
                case AccountType.User:
                    Menu.Account();
                    break;
                case AccountType.Employee:
                    Menu.EmployeeAccount();
                    break;
                case AccountType.Admin:
                    Menu.AdminAccount();
                    break;
            }
        }
    }
}