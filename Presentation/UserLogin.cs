using System.Text.RegularExpressions;

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
            AccountFunctionality.ErrorMessage("No account found with that email and password, please try again.");
            Menu.Start();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Welcome back " + acc.FullName);
            Console.WriteLine("Your email address is " + acc.EmailAddress);
            if (acc.IsAdmin) Menu.AdminAccount();
            if (acc.IsEmployee) Menu.EmployeeAccount();
            else Menu.Account();
        }
    }

    private static bool PasswordCheck(string password)
    {
        string specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-""";

        if (password.Length < 6 || password.Length > 50 ||
            !(password.Any(char.IsUpper) && password.Any(char.IsLower)) ||
            password.Contains(" ") ||
            !password.Any(ch => specialCharacters.Contains(ch)) ||
            !password.Any(char.IsNumber))
        {
            Console.WriteLine("Password does not meet the standard requirements.");
            Console.WriteLine("Password needs at least:\n- 6 characters\n- 1 symbol\n- 1 upper and lower case letter\n- a number");
            return false;
        }

        return true;
    }

    private static bool SanitizeEmailValidator(string email)
    {
        const string emailRegex = @"^[a-zA-Z0-9._+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        string sanitizedEmail = Regex.Replace(email, @"[^a-zA-Z0-9.@+-]", "");

        if (!Regex.IsMatch(sanitizedEmail, emailRegex))
        {
            Console.WriteLine("Email Address does not meet the standard requirements.");
            Console.WriteLine("Please check the following conditions:\n- The email format should be 'example@example.com'");
            return false;
        }
        return true;
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
        string UnhashedPassword = AccountFunctionality.HidePassword();
        string password_1 = AccountsLogic.GetHashedSHA256(UnhashedPassword);
        Console.Write("Please enter your password again\n>> ");
        string password_2 = AccountsLogic.GetHashedSHA256(AccountFunctionality.HidePassword());

        if (password_1 != password_2)
        {
            AccountFunctionality.ErrorMessage("Passwords don't match, please try again.");
            MakeAccount(type, back_to_menu);
        }
        else if (PasswordCheck(UnhashedPassword) && SanitizeEmailValidator(email))
        {
            List<AccountModel> dataList = AccountsAccess.LoadAll();
            if (dataList.Any(data => data.EmailAddress == email))
                Console.WriteLine("An account with the same email address already exists. Please choose a different email address.");
            else
            {
                int highestId = dataList.Max(data => data.Id);
                AccountModel newData;
                switch (type)
                {
                    case AccountType.Admin:
                        newData = new AccountModel(highestId + 1, email, password_1, full_name, false, true); // admin is automatically false
                        break;
                    case AccountType.Employee:
                        newData = new AccountModel(highestId + 1, email, password_1, full_name, true, false); // admin is automatically false
                        break;
                    default:
                        newData = new AccountModel(highestId + 1, email, password_1, full_name); // admin is automatically false
                        break;
                }
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
        else
        {
            MakeAccount(type, back_to_menu);
        }
    }
}