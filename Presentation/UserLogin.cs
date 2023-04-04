using Newtonsoft.Json;
static class UserLogin
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine()!;
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine()!;
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            Console.WriteLine("Welcome back " + acc.FullName);
            Console.WriteLine("Your email number is " + acc.EmailAddress);
            Menu.Account();

            //Write some code to go back to the menu
            //Menu.Start();
        }
        else
        {
            Console.WriteLine("No account found with that email and password");
            Menu.Start();
        }
    }

    public static void MakeAccount()
    {
        Console.WriteLine("Please enter your full name");
        string full_name = Console.ReadLine()!;
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine()!;
        Console.WriteLine("Please enter your password");
        string password_1 = Console.ReadLine()!;
        Console.WriteLine("Please enter your password again");
        string password_2 = Console.ReadLine()!;

        if (password_1 != password_2)
        {
            Console.WriteLine("Password is not the same, please try again..");
            UserLogin.MakeAccount();
        }
        else
        {
            string reader = File.ReadAllText("DataSources/accounts.json");
            List<AccountModel> dataList = JsonConvert.DeserializeObject<List<AccountModel>>(reader)!;
            if (dataList.Any(data => data.EmailAddress == email))
                Console.WriteLine("An account with the same email address already exists. Please choose a different email address.");

            else
            {
                int highestId = dataList.Max(data => data.Id);
                AccountModel newData = new AccountModel(highestId + 1, email, password_1, full_name);
                dataList.Add(newData);
                string updatedJson = JsonConvert.SerializeObject(dataList, Formatting.Indented);
                StreamWriter writer = new("DataSources/accounts.json");
                writer.Write(updatedJson);
                writer.Close();
            }
            Menu.Start();
        }
    }
}