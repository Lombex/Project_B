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
            if (User.EmailAddress == EmailAddress) User.Password = AccountsLogic.GetHashedSHA256(ChangedPassword);
            else throw new Exception("This user doesnt exist!");
        }
    }

    // Check user password password
    public string CheckUserPassword(string EmailAddress)
    {
        foreach (AccountModel User in AccountsAccess.LoadAll())
        {
            if (User.EmailAddress == EmailAddress) return User.Password;
            else throw new Exception("Email does not exists");
        }
        return null!;
    }

    // Create new account method 
    public void create_account()
    {
        UserLogin.MakeAccount(true);
    }

}