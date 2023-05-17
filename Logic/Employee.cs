public static class Employee
{
    public static virtual void ChangeUserPassword(string EmailAddress)
    {
        List<AccountModel> account_list = AccountsAccess.LoadAll();
        foreach (AccountModel User in account_list)
        {
            if (User.EmailAddress == EmailAddress)
            {
                if(User.IsEmployee || User.IsAdmin)
                {
                    AccountFunctionality.ErrorMessage("Account is not a user, ask an admin to change their password.");
                    Menu.EmployeeAccount();
                }
                else
                {
                    Console.Write("Please enter a new password\n>> ");
                    var Password1 = AccountFunctionality.HidePassword();
                    Console.Write("Please enter it again\n>> ");
                    var Password2 = AccountFunctionality.HidePassword();
                    while (Password1 != Password2)
                    {
                        Console.Write("Passwords didn't match. Please enter a new password.\n>> ");
                        Password1 = AccountFunctionality.HidePassword();
                        Console.Write("Please enter it again\n>> ");
                        Password2 = AccountFunctionality.HidePassword();
                    }
                    User.Password = AccountsLogic.GetHashedSHA256(Password1!);
                    AccountsAccess.WriteAll(account_list);
                    return;
                }
            }
        }
        AccountFunctionality.ErrorMessage("This Account does either not exist or does not match with given input!\npress Enter to confirm.");
        Menu.EmployeeAccount();

    }
    public static void Create_account()
    {
        Console.Clear();
        AccountFunctionality.PrintBanner();

    }
}