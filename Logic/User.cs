using Newtonsoft.Json;

public class User
{
    // Create method for edit own account data

    // Create method cancel booking 

    // Create method book flight 

    // Create method see reservation information
    public void ChangeName(bool isAdmin = false)
    {
        if (!isAdmin)
        {
            Console.Write("Please enter your password: ");
            string? password = AccountsLogic.GetHashedSHA256(AccountFunctionality.HidePassword());
            AccountModel? acc = AccountsLogic.CheckLogin(UserLogin.AccountInfo!.EmailAddress, password);
            if (acc == null)
            {
                Console.WriteLine("This account does not exits");
                ChangeName();
            }
        }
        else
        {

            Console.Write("Give the users email address: ");
            string EmailAddress = Console.ReadLine()!;
            var account = ViewFlights.accountList.FirstOrDefault(a => a.EmailAddress == EmailAddress);
            if (account != null)
            {
                Console.WriteLine("Give users new name: ");
                string NewName = Console.ReadLine()!;
                account!.FullName = NewName;
                ViewFlights.accountList[account.Id - 1] = account;
                AccountsAccess.WriteAll(ViewFlights.accountList);
                Console.WriteLine("User has been changed!");
                Console.Write("\nPress Enter to continue...");
                Console.ReadLine();
                Menu.AdminAccount();

            }
            else
            {
                Console.WriteLine("Coudnt find email please try again!");
                ChangeName(true);
            }
        }

        Console.Write("What name do you want use? ");
        string NewUsername = Console.ReadLine()!;
        Console.WriteLine("Confirm your name.");
        string ConfirmUsername = Console.ReadLine()!;
        if (NewUsername == ConfirmUsername)
        {
            Console.WriteLine($"Username has been changed to {NewUsername}");
            UserLogin.AccountInfo!.FullName = NewUsername;
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
    public void ChangePassword()
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
    public void ChangeEmail(bool isAdmin = false)
    {
        if (!isAdmin)
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
        }
        else
        {
            List<AccountModel> account_list = AccountsAccess.LoadAll();
            Console.Write("Give the users email address: ");
            string EmailAddress = Console.ReadLine()!;
            foreach (AccountModel account in account_list)
            {
                if (account.EmailAddress == EmailAddress)
                {
                    Console.WriteLine("Give users new email: ");
                    string _NewEmail = Console.ReadLine()!;
                    account.EmailAddress = _NewEmail;
                    AccountsAccess.WriteAll(account_list);

                }
                else
                {
                    Console.WriteLine("Coudnt find email please try again!");
                    ChangeName(true);
                }
            }
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