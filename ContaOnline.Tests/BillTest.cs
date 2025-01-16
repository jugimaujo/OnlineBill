using System;
using OnlineBill.Repository;
using OnlineBill.Domain.Models;

namespace OnlineBill.Tests;

[TestClass]
public class BillTest
{
    BillRepository billRepository = new BillRepository();

    #region Bill Repository Test

    [TestMethod]
    public void BillAddTest()
    {
        var bill = new Bill
        {
            Id = "test",
            UserId = "test",
            CheckingAccountId = "test",
            Type = PayReceive.Pay,
            CategoryId = "test",
            ContactId = "test",
            Description = "Test bill",
            DueDate = DateTime.Now,
            Value = 1,
            PaymentDate = DateTime.Now,
            Discount = 1,
            Increase = 1,
            PaidValue = 1
        };

        billRepository.Add(bill);
    }

    [TestMethod]
    public void BillUpdateTest()
    {
        var bill = new Bill
        {
            Id = "test",
            UserId = "test",
            CheckingAccountId = "test",
            Type = PayReceive.Receive,
            CategoryId = "test",
            ContactId = "test",
            Description = "Test bill Updated",
            DueDate = DateTime.Now,
            Value = 2,
            PaymentDate = DateTime.Now,
            Discount = 2,
            Increase = 2,
            PaidValue = 2
        };

        billRepository.Update(bill);
    }

    [TestMethod]
    public void BillDeleteTest()
    {
        billRepository.Delete("test");
    }

    [TestMethod]
    public void BillGetAllTest()
    {
        var list = billRepository.GetAll("test");
        foreach (var bill in list)
        {
            Console.WriteLine(bill.Id);
            Console.WriteLine(bill.UserId);
            Console.WriteLine(bill.CheckingAccountId);
            Console.WriteLine(bill.Type);
            Console.WriteLine(bill.CategoryId);
            Console.WriteLine(bill.ContactId);
            Console.WriteLine(bill.Description);
            Console.WriteLine(bill.DueDate);
            Console.WriteLine(bill.Value);
            Console.WriteLine(bill.PaymentDate);
            Console.WriteLine(bill.Discount);
            Console.WriteLine(bill.Increase);
            Console.WriteLine(bill.PaidValue);
        }
    }

    [TestMethod]
    public void BillGetByIdTest()
    {
        var bill = billRepository.GetById("test");
        if (bill != null)
        {
            Console.WriteLine(bill.Id);
            Console.WriteLine(bill.UserId);
            Console.WriteLine(bill.CheckingAccountId);
            Console.WriteLine(bill.Type);
            Console.WriteLine(bill.CategoryId);
            Console.WriteLine(bill.ContactId);
            Console.WriteLine(bill.Description);
            Console.WriteLine(bill.DueDate);
            Console.WriteLine(bill.Value);
            Console.WriteLine(bill.PaymentDate);
            Console.WriteLine(bill.Discount);
            Console.WriteLine(bill.Increase);
            Console.WriteLine(bill.PaidValue);
        }
    }

    #endregion
}
