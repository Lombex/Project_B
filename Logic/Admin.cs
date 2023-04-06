using Newtonsoft.Json;
public class Admin : User
{

    // Create new method edit customer account data

    // Edit, remove, add booking 

    // Create new method add and remove flights

    // Create new method change flight data

    public void ChangeUserPassword(string EmailAddress, string ChangedPassword)
    {
        foreach (AccountModel User in AccountsAccess.LoadAll())
        {
            if (User.EmailAddress == EmailAddress)
            {
                User.Password = AccountsLogic.GetHashedSHA256(ChangedPassword);
                Menu.AdminAccount();
            }
            else
            {
                Console.WriteLine("This Account does either not exists or does not match with given input!");
                Menu.AdminAccount();
            }
        }
    }
    public void Create_account()
    {
        Console.WriteLine("\n+-------------------------+");
        Console.WriteLine("Choose your option and press enter");
        Console.WriteLine("1. Create User Account\n2. Create Employee Account");
        Console.WriteLine("+-------------------------+");
        int Option = Convert.ToInt32(Console.ReadLine());
        if (Option == 1) UserLogin.MakeAccount(UserLogin.AccountType.User);
        else if (Option == 2) UserLogin.MakeAccount(UserLogin.AccountType.Employee);
        else
        {
            Console.WriteLine("This is not an option please choose carefully!");
            Create_account();
        }
    }
}