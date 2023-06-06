using Newtonsoft.Json;

public class User
{
    // Create method for edit own account data

    // Create method cancel booking 

    // Create method book flight 

    // Create method see reservation information

    public static void UserEditMenu()
    {
        List<string> Options = new List<string> { "Enter 1 to change name", "Enter 2 to change password", "Enter 3 to change email", "Enter 4 to go back" };
        Console.WriteLine("\n+-------------------------+");
        foreach (string option in Options) Console.WriteLine(option);
        Console.WriteLine("+-------------------------+");

        Console.Write("Select a option: ");
        string SelectedOption = Console.ReadLine()!;
        
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
                Console.WriteLine("This is not a option!");
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
            string JsonData = File.ReadAllText("accounts.json");
            AccountModel data = JsonConvert.DeserializeObject<AccountModel>(JsonData)!;
            data.FullName = NewUsername;
            string updatedJsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText("accounts.json", updatedJsonData);
            UserLogin.AccountInfo.FullName = NewUsername;
        }
        else
        {
            Console.WriteLine("The usernames given dont match");
            ChangeName();
        }

    }
    private static void ChangePassword() 
    {

    }
    private static void ChangeEmail() 
    {

    }

}