public class AccountsAccess : DataAccess<AccountModel>
{
    public AccountsAccess()
    {
        path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"DataSources/accounts.json"));
    }
}