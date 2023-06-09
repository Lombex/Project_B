using System.Security.Cryptography;
using System.Text;

//This class is not static so later on we can use inheritance and interfaces
static class AccountsLogic
{
    private static List<AccountModel> _accounts;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    public static AccountModel? CurrentAccount { get; private set; }

    static AccountsLogic()
    {
        _accounts = AccountsAccess.LoadAll();
    }

    private static void Update()
    {
        _accounts = AccountsAccess.LoadAll();
    }


    public static void UpdateList(AccountModel acc)
    {
        //Find if there is already an model with the same id
        int index = _accounts.FindIndex(s => s.Id == acc.Id);

        if (index != -1)
        {
            //update existing model
            _accounts[index] = acc;
        }
        else
        {
            //add new model
            _accounts.Add(acc);
        }
        AccountsAccess.WriteAll(_accounts);
    }

    public static AccountModel? GetById(int id)
    {
        return _accounts.Find(i => i.Id == id);
    }
    public static string GetHashedSHA256(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++) builder.Append(bytes[i].ToString("x2"));
            return builder.ToString();
        }
    }
    public static AccountModel? CheckLogin(string email, string? password)
    {
        if (email == null || password == null) throw new Exception("Either email or password is null");
        Update();
        CurrentAccount = _accounts.Find(i => i.EmailAddress == email && i.Password == password);
        return CurrentAccount;
    }

}


