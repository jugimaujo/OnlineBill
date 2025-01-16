using System;
using OnlineBill.Domain.Models;
using OnlineBill.Repository;

namespace OnlineBill.Tests;

[TestClass]
public class CheckingAccountTest
{
    CheckingAccountRepository checkingAccountRepository = new CheckingAccountRepository();

    #region Checking Account Repository Test

    [TestMethod]
    public void CheckingAccountAddTest()
    {
        var checkingAccount = new CheckingAccount
        {
            Id = "test",
            UserId = "test",
            Description = "Testing Account"
        };
        checkingAccountRepository.Add(checkingAccount);
    }

    [TestMethod]
    public void CheckingAccountUpdateTest()
    {
        var checkingAccount = new CheckingAccount
        {
            Id = "test",
            Description = "Updated Testing Account"
        };
        checkingAccountRepository.Update(checkingAccount);
    }

    [TestMethod]
    public void CheckingAccountDeleteTest()
    {
        checkingAccountRepository.Delete("test");
    }

    [TestMethod]
    public void CheckingAccountGetAllTest()
    {
        var list = checkingAccountRepository.GetAll("test");
        foreach (var checkingAccount in list)
        {
            Console.WriteLine(checkingAccount.Id);
            Console.WriteLine(checkingAccount.UserId);
            Console.WriteLine(checkingAccount.Description);
        }
    }

    [TestMethod]
    public void CheckingAccountGetByIdTest()
    {
        var checkingAccount = checkingAccountRepository.GetById("test");
        if (checkingAccount != null)
        {
            Console.WriteLine(checkingAccount.Id);
            Console.WriteLine(checkingAccount.UserId);
            Console.WriteLine(checkingAccount.Description);
        }
    }

    #endregion
}
