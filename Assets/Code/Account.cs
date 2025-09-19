using System;
using System.Collections.Generic;

[System.Serializable]
public class Account
{
    public string username;
    public string password;
}

[System.Serializable]
public class AccountList
{
    public List<Account> accounts = new List<Account>();
}
