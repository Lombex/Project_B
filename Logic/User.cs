using Newtonsoft.Json;

public class User
{
    // Create method for edit own account data

    // Create method cancel booking 

    // Create method book flight 

    // Create method see reservation information

    public static void UserEditMenu()
    {
        Console.Clear();
        List<string> Options = new List<string> { "Enter 1 to change name", "Enter 2 to change password", "Enter 3 to change email", "Enter 4 to go back" };
        Console.WriteLine("\n+-------------------------+");
        foreach (string option in Options) Console.WriteLine(option);
        Console.WriteLine("+-------------------------+");

        string SelectedOption = AccountFunctionality.GetInput();

        switch (SelectedOption)
        {
            case "1":
                ChangeName();
                break;
            case "2":
                ChangePassword();
                break;
            case "3":
                ChangeEmail();
                break;
            case "4":
                Menu.Account();
                break;
            default:
                Console.WriteLine("This is not an option!");
                UserEditMenu();
                break;
        }
    }
    private static void ChangeName()
    {
        Console.Write("Please enter your password: ");
        string? password = AccountsLogic.GetHashedSHA256(AccountFunctionality.HidePassword());
        AccountModel? acc = AccountsLogic.CheckLogin(UserLogin.AccountInfo!.EmailAddress, password);
        if (acc == null)
        {
            AccountFunctionality.ErrorMessage("Your password was incorrect.");
            ChangeName();
        }
        Console.Write("What name do you want to use? ");
        string NewUsername = AccountFunctionality.GetInput();
        Console.WriteLine("Confirm your name.");
        string ConfirmUsername = AccountFunctionality.GetInput();
        if (NewUsername == ConfirmUsername)
        {
            Console.WriteLine($"Username has been changed to {NewUsername}");
            UserLogin.AccountInfo.FullName = NewUsername;
            ViewFlights.accountList[UserLogin.AccountInfo.Id - 1] = UserLogin.AccountInfo;
            AccountsAccess.WriteAll(ViewFlights.accountList);
        }
        else
        {
            AccountFunctionality.ErrorMessage("The usernames given don't match, please try again.");
            ChangeName();
        }

    }
    private static void ChangePassword()
    {
        Console.Write("Please enter your old password: ");
        string? password = AccountsLogic.GetHashedSHA256(AccountFunctionality.HidePassword());
        AccountModel? acc = AccountsLogic.CheckLogin(UserLogin.AccountInfo!.EmailAddress, password);
        if (acc == null)
        {
            Console.WriteLine("This account does not exist");
            ChangePassword();
        }
        Console.Write("Enter new password: ");
        string? NewPassword = AccountsLogic.GetHashedSHA256(AccountFunctionality.HidePassword());
        Console.Write("Confirm your password: ");
        string? ConfirmedPassword = AccountsLogic.GetHashedSHA256(AccountFunctionality.HidePassword());
        if (NewPassword == ConfirmedPassword && UserLogin.PasswordCheck(NewPassword))
        {
            Console.WriteLine($"Password has been successfully changed");
            UserLogin.AccountInfo.Password = NewPassword;
            ViewFlights.accountList[UserLogin.AccountInfo.Id - 1] = UserLogin.AccountInfo;
            AccountsAccess.WriteAll(ViewFlights.accountList);
        }
        else
        {
            AccountFunctionality.ErrorMessage("The password given don't match, please try again.");
            ChangePassword();
        }
    }
    private static void ChangeEmail()
    {   
    }

}