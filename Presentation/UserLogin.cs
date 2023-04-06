static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();
    public static void Start()
    {
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine()!;
        Console.WriteLine("Please enter your password");
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
    public static void MakeAccount(AccountType type)
    {
        Console.WriteLine("Please enter your full name");
        string full_name = Console.ReadLine()!;
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine()!;
        Console.WriteLine("Please enter your password");
        string password_1 = AccountsLogic.GetHashedSHA256(AccountFunctionality.HidePassword());
        Console.WriteLine("Please enter your password again");
        string password_2 = AccountsLogic.GetHashedSHA256(AccountFunctionality.HidePassword());

        if (password_1 != password_2)
        {
            Console.WriteLine("Password is not the same, please try again..");
            UserLogin.MakeAccount(type);
        }
        else
        {
            List<AccountModel> dataList = AccountsAccess.LoadAll();
            if (dataList.Any(data => data.EmailAddress == email))
                Console.WriteLine("An account with the same email address already exists. Please choose a different email address.");
            else
            {
                int highestId = dataList.Max(data => data.Id);
                bool IsEmployee = false;
                if (type == AccountType.Employee) IsEmployee = true;
                AccountModel newData = new AccountModel(highestId + 1, email, password_1, full_name, IsEmployee);
                dataList.Add(newData);
                AccountsAccess.WriteAll(dataList);
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