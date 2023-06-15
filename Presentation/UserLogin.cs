using System.Diagnostics.Metrics;
using System.Globalization;
using System.Net;
using System.Numerics;
using System.Text.RegularExpressions;

static class UserLogin
{
    private static AccountsAccess accountsAccess = new AccountsAccess();
    public static AccountModel? AccountInfo { get; set; } // AccountInformation
    public static void Start()
    {
        string email = AccountFunctionality.GetInput("Welcome to the login page\nPlease enter your email address.");
        Console.Write("Please enter your password\n>> ");
        string? password = AccountsLogic.GetHashedSHA256(AccountFunctionality.HidePassword());
        AccountModel? acc = AccountsLogic.CheckLogin(email, password);
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
            AccountInfo = acc;
            if (acc.IsAdmin) Menu.AdminAccount();
            if (acc.IsEmployee) Menu.EmployeeAccount();
            else Menu.Account();
        }
    }
    public static bool PasswordCheck(string password)
    {
        string specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-""";

        if (password.Length < 6 || password.Length > 50 ||
            !(password.Any(char.IsUpper) && password.Any(char.IsLower)) ||
            password.Contains(" ") ||
            !password.Any(ch => specialCharacters.Contains(ch)) ||
            !password.Any(char.IsNumber))
        {
            AccountFunctionality.ErrorMessage("Password does not meet the standard requirements.\nPassword needs at least:\n- 6 characters\n- 1 symbol\n- 1 upper and lower case letter\n- a number");
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
            AccountFunctionality.ErrorMessage("Email Address does not meet the standard requirements.\nPlease check the following conditions:\n- The email format should be 'example@example.com'");
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
        string full_name = AccountFunctionality.GetInput("Please enter your full name");

        var textinfo = new CultureInfo("en-US", false).TextInfo;
        full_name = textinfo.ToTitleCase(full_name.ToLower());

        string email = AccountFunctionality.GetInput("Please enter your email address");
        List<AccountModel> dataList = accountsAccess.LoadAll();
        if (dataList.Any(data => data.EmailAddress == email))
        {
            AccountFunctionality.ErrorMessage("An account with the same email address already exists. Please choose a different email address.");
            MakeAccount(type, back_to_menu);
        }
        Console.Write("Please enter your password\n>> ");
        string UnhashedPassword = AccountFunctionality.HidePassword();
        string password_1 = AccountsLogic.GetHashedSHA256(UnhashedPassword);
        Console.Write("Please enter your password again\n>> ");
        string password_2 = AccountsLogic.GetHashedSHA256(AccountFunctionality.HidePassword());
        Console.Write("Do you have a disability\n>> ");


        if (password_1 != password_2)
        {
            AccountFunctionality.ErrorMessage("Passwords don't match, please try again.");
            MakeAccount(type, back_to_menu);
        }
        else if (PasswordCheck(UnhashedPassword) && SanitizeEmailValidator(email))
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
            AccountInfo = newData;
            accountsAccess.WriteAll(dataList);
            if (back_to_menu == false) Menu.AdminAccount();

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
        else MakeAccount(type, back_to_menu);
    }
}