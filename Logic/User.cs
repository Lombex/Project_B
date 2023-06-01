public class User
{
    // Create method for edit own account data

    // Create method cancel booking 

    // Create method book flight 

    // Create method see reservation information

    public void UserEditMenu()
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

    private void ChangeName() 
    {
        Console.WriteLine("What name do you want");

    }
    private void ChangePassword() 
    {

    }
    private void ChangeEmail() 
    {

    }

}