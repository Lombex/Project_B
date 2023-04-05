using Newtonsoft.Json;
public class Admin : User
{

    // Create new method edit customer account data

    // Edit, remove, add booking 

    // Create new method add and remove flights

    // Create new method change flight data

    // Change user password function 
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

    // Create new account method 
    public void Create_account()
    {
        UserLogin.MakeAccount(true);
    }

}