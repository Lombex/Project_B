using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

static class AccountsLogic
{
    private static List<AccountModel> _accounts;
    private static AccountsAccess accountsAccess = new AccountsAccess();

    public static AccountModel? CurrentAccount { get; private set; }

    static AccountsLogic()
    {
        _accounts = accountsAccess.LoadAll();
    }

    private static void Update()
    {
        _accounts = accountsAccess.LoadAll();
    }

    public static void UpdateList(AccountModel acc)
    {
        int index = _accounts.FindIndex(s => s.Id == acc.Id);

        if (index != -1)
        {
            _accounts[index] = acc;
        }
        else
        {
            _accounts.Add(acc);
        }
        accountsAccess.WriteAll(_accounts);
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