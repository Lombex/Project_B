using System.Diagnostics;
using System.Linq;
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
    public static bool PasswordCheck(string password)
    {
        string specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
        if (password.Length < 6 || password.Length > 50) return false;
        if (!(password.Any(char.IsUpper) && password.Any(char.IsLower))) return false;
        if (password.Contains(" ")) return false;
        if (!password.Any(ch => specialCharacters.Contains(ch))) return false;
        if (!password.Any(char.IsNumber)) return false;
        return true;
    }
    public static string SanitizeEmailValidator(string email)
    {
        const string emailRegex = @"^[a-zA-Z0-9._+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        string sanitizedEmail = Regex.Replace(email, @"[^a-zA-Z0-9.@+-]", "");
        if (Regex.IsMatch(sanitizedEmail, emailRegex)) return sanitizedEmail;
        else throw new ArgumentException("Invalid email address");
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
            Console.WriteLine("Password is not the same, please try again..");
            UserLogin.MakeAccount(type, back_to_menu);
        }
        else if (PasswordCheck(UnhashedPassword))
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
            Console.WriteLine("Password does not meet the standard requirements.");
            Console.WriteLine("- Needs atleast 6 characters.\n- Needs atleast 1 symbol\n- Needs atleast 1 upper and lower case letter\n- Needs a number");
            MakeAccount(type, back_to_menu);

        }
    }
}