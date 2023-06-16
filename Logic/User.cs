using Newtonsoft.Json;
using System.Globalization;

public class User
{
    protected static AccountsAccess accountsAccess = new AccountsAccess();

    // Create method for edit own account data

    // Create method cancel booking 

    // Create method book flight 

    // Create method see reservation information
    public void ChangeName(bool isAdmin = false)
    {
        if (!isAdmin)
        {
            if (!CheckPassword())
            {
                AccountFunctionality.ErrorMessage("This password is incorrect, please try again.");
                ChangeName();
                return;
            }
        }
        else
        {


            string EmailAddress = AccountFunctionality.GetInput("Give the user's email address: ");
            var account = ViewFlights.accountList.FirstOrDefault(a => a.EmailAddress == EmailAddress);
            if (account != null)
            {
                Console.WriteLine("Give user's new name: ");
                string NewName = Console.ReadLine()!;
                account!.FullName = NewName;
                ViewFlights.accountList[account.Id - 1] = account;
                accountsAccess.WriteAll(ViewFlights.accountList);
                Console.WriteLine("User has been changed!");
                Console.Write("\nPress Enter to continue...");
                Console.ReadLine();
                Menu.AdminAccount();

            }
            else
            {
                AccountFunctionality.ErrorMessage("Could not find that email, please try again!");
                ChangeName(isAdmin);
            }
        }

        string NewUsername = AccountFunctionality.GetInput("What name do you want to use? ");
        string ConfirmUsername = AccountFunctionality.GetInput("Please enter your name again.");
        if (NewUsername == ConfirmUsername)
        {
            // uses TextInfo to turn names from "john doe" to "John Doe"
            var textinfo = new CultureInfo("en-US", false).TextInfo;
            NewUsername = textinfo.ToTitleCase(NewUsername.ToLower());

            Console.WriteLine($"Username has been changed to {NewUsername}");
            UserLogin.AccountInfo!.FullName = NewUsername;
            ViewFlights.accountList[UserLogin.AccountInfo.Id - 1] = UserLogin.AccountInfo;
            accountsAccess.WriteAll(ViewFlights.accountList);
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }
        else
        {
            AccountFunctionality.ErrorMessage("The usernames given don't match.");
            ChangeName();
        }
    }
    public void ChangePassword()
    {
        if (!CheckPassword())
        {
            AccountFunctionality.ErrorMessage("Your password was incorrect.");
            ChangePassword();
            return;
        }
        Console.Write("Enter new password: \n>> ");
        string? NewPassword = AccountFunctionality.HidePassword();
        Console.Write("Confirm your password: \n>> ");
        string? ConfirmedPassword = AccountFunctionality.HidePassword();
        if (UserLogin.PasswordCheck(NewPassword) && NewPassword == ConfirmedPassword)
        {
            Console.WriteLine($"Password has been successfully changed");
            UserLogin.AccountInfo!.Password = AccountsLogic.GetHashedSHA256(NewPassword);
            ViewFlights.accountList[UserLogin.AccountInfo.Id - 1] = UserLogin.AccountInfo;
            accountsAccess.WriteAll(ViewFlights.accountList);
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }
        else
        {
            AccountFunctionality.GetInput("The passwords given don't match.");
            ChangePassword();
        }
    }

    public void ChangeEmail(bool isAdmin = false)
    {
        if (!isAdmin)
        {
            if (!CheckPassword())
            {
                AccountFunctionality.ErrorMessage("This password is incorrect, please try again.");
                ChangeEmail();
                return;
            }
        }
        else
        {
            string EmailAddress = AccountFunctionality.GetInput("Give the user's email address: ");
            var account = ViewFlights.accountList.FirstOrDefault(a => a.EmailAddress == EmailAddress);
            if (account != null)
            {
                Console.WriteLine("Give user's new Email: ");
                string NewName = Console.ReadLine()!;
                account!.EmailAddress = NewName;
                ViewFlights.accountList[account.Id - 1] = account;
                accountsAccess.WriteAll(ViewFlights.accountList);
                Console.WriteLine("User has been changed!");
                Console.Write("\nPress Enter to continue...");
                Console.ReadLine();
                Menu.AdminAccount();
            }
            else
            {
                AccountFunctionality.ErrorMessage("Could not find that email, please try again!");
                ChangeEmail(isAdmin);
            }
        }
        string EmailName = AccountFunctionality.GetInput("What name do you want to use? ");
        string ConfirmEmail = AccountFunctionality.GetInput("Please enter your name again.");
        if (EmailName == ConfirmEmail)
        {
            // uses TextInfo to turn names from "john doe" to "John Doe"
            var textinfo = new CultureInfo("en-US", false).TextInfo;
            EmailName = textinfo.ToTitleCase(EmailName.ToLower());

            Console.WriteLine($"Email has been changed to {EmailName}");
            UserLogin.AccountInfo!.EmailAddress = EmailName;
            ViewFlights.accountList[UserLogin.AccountInfo.Id - 1] = UserLogin.AccountInfo;
            accountsAccess.WriteAll(ViewFlights.accountList);
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }
        else
        {
            AccountFunctionality.ErrorMessage("The email given don't match.");
            ChangeEmail();
        }
    }

    /*public void ChangeEmail(bool isAdmin = false)
    {
        string NewEmail = "";
        string ConfirmEmail = "";
        if (!isAdmin)
        {
            if (!CheckPassword())
            {
                AccountFunctionality.ErrorMessage("Your password was incorrect, please try again.");
                ChangeEmail(isAdmin);
                return;
            }
        }
        else
        {

            List<AccountModel> account_list = accountsAccess.LoadAll();
            string EmailAddress = AccountFunctionality.GetInput("Enter the user's email address: ");
            AccountModel? updatedAccount = account_list.FirstOrDefault(a => a.EmailAddress == EmailAddress);
            if (updatedAccount != null)
            {
                NewEmail = AccountFunctionality.GetInput("Enter user's new email: ");
                ConfirmEmail = AccountFunctionality.GetInput("Please confirm your new email: ");
                if (NewEmail == ConfirmEmail)
                {
                    Console.WriteLine($"Email has been changed to {NewEmail}.");
                    updatedAccount.EmailAddress = NewEmail;
                    UserLogin.AccountInfo!.EmailAddress = NewEmail;
                    accountsAccess.WriteAll(account_list);

                    Console.Write("\nPress Enter to continue...");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Given emails don't match!");
                    ChangeEmail();
                }

            }
            else
            {
                Console.WriteLine("Couldn't find that user, please try again!");
                ChangeName(true);
            }
        }

    }*/

    private bool CheckPassword(bool isAdmin = false)
    {
        if (!isAdmin) Console.Write("Please enter your password\n>> ");
        else Console.WriteLine("Please enter the user's password");
        string? password = AccountsLogic.GetHashedSHA256(AccountFunctionality.HidePassword());
        AccountModel? acc = AccountsLogic.CheckLogin(UserLogin.AccountInfo!.EmailAddress, password);
        return acc != null;
    }
}