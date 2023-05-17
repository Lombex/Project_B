public static class User
{
    public static void ChangeMyPassword(int id)
    {
        List<AccountModel> account_list = AccountsAccess.LoadAll();

        Console.Write("Please enter a new password\n>> ");
        var Password1 = AccountFunctionality.HidePassword();
        Console.Write("Please enter it again\n>> ");
        var Password2 = AccountFunctionality.HidePassword();
        while (Password1 != Password2)
        {
            AccountFunctionality.GetInput("Passwords didn't match. Please enter a new password.");
            Password1 = AccountFunctionality.HidePassword();
            AccountFunctionality.GetInput("Please enter it again.");
            Password2 = AccountFunctionality.HidePassword();
        }
        while(!UserLogin.PasswordCheck(Password1))
        user.Password = AccountsLogic.GetHashedSHA256(Password1!);
        AccountsAccess.WriteAll(account_list);
        return;
    }
}