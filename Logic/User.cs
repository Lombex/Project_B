using Newtonsoft.Json;

public class User
{
    // Create method for edit own account data

    // Create method cancel booking 

    // Create method book flight 

    // Create method see reservation information

    public static void ChangeName()
    {
        Console.Write("Please enter your password: ");
        string? password = AccountsLogic.GetHashedSHA256(AccountFunctionality.HidePassword());
        AccountModel? acc = AccountsLogic.CheckLogin(UserLogin.AccountInfo!.EmailAddress, password);
        if (acc == null)
        {
            Console.WriteLine("This account does not exits");
            ChangeName();
        }
        Console.Write("What name do you want use? ");
        string NewUsername = Console.ReadLine()!;
        Console.WriteLine("Confirm your name.");
        string ConfirmUsername = Console.ReadLine()!;
        if (NewUsername == ConfirmUsername)
        {
            Console.WriteLine($"Username has been changed to {NewUsername}");
            UserLogin.AccountInfo.FullName = NewUsername;
            ViewFlights.accountList[UserLogin.AccountInfo.Id - 1] = UserLogin.AccountInfo;
            AccountsAccess.WriteAll(ViewFlights.accountList);
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("The usernames given dont match");
            ChangeName();
        }

    }
    public static void ChangePassword()
    {
        Console.Write("Please enter your old password: ");
        string? password = AccountsLogic.GetHashedSHA256(AccountFunctionality.HidePassword());
        AccountModel? acc = AccountsLogic.CheckLogin(UserLogin.AccountInfo!.EmailAddress, password);
        if (acc == null)
        {
            Console.WriteLine("This account does not exits");
            ChangePassword();
        }
        Console.Write("Enter new password: ");
        string? NewPassword = AccountFunctionality.HidePassword();
        Console.Write("Confirm your password: ");
        string? ConfirmedPassword = AccountFunctionality.HidePassword();
        if (UserLogin.PasswordCheck(NewPassword) && NewPassword == ConfirmedPassword)
        {
            Console.WriteLine($"Password has been successfully changed");
            UserLogin.AccountInfo.Password = AccountsLogic.GetHashedSHA256(NewPassword);
            ViewFlights.accountList[UserLogin.AccountInfo.Id - 1] = UserLogin.AccountInfo;
            AccountsAccess.WriteAll(ViewFlights.accountList);
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("The password given dont match");
            ChangePassword();
        }
    }
    public static void ChangeEmail()
    {
        Console.Write("Please enter your password: ");
        string? password = AccountsLogic.GetHashedSHA256(AccountFunctionality.HidePassword());
        Console.Write("Enter your current Email: ");
        string CurrentEmail = Console.ReadLine()!;
        AccountModel? acc = AccountsLogic.CheckLogin(CurrentEmail, password);
        if (acc == null)
        {
            Console.WriteLine("This account does not exits");
            ChangeEmail();
        }

        Console.Write("Enter your new email: ");
        string NewEmail = Console.ReadLine()!;
        Console.Write("Confirm your new email: ");
        string ConfirmEmail = Console.ReadLine()!;

        if (NewEmail == ConfirmEmail)
        {
            Console.WriteLine($"Email has been changed to {NewEmail}");
            UserLogin.AccountInfo!.EmailAddress = NewEmail;
            ViewFlights.accountList[UserLogin.AccountInfo.Id - 1] = UserLogin.AccountInfo;
            AccountsAccess.WriteAll(ViewFlights.accountList);
            Console.Write("\nPress Enter to continue...");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Email does not match!");
            ChangeEmail();
        }
    }

}