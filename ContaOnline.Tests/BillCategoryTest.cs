using OnlineBill.Repository;
using OnlineBill.Domain.Models;

namespace OnlineBill.Tests;

[TestClass]
public class BillCategoryTest
{
    BillCategoryRepository billCategoryRepository = new BillCategoryRepository();

    #region Bill Category Repository Test

    [TestMethod]
    public void BillCategoryAddTest()
    {
        var billCategory = new BillCategory
        {
            Id = "test",
            UserId = "testUser",
            Name = "Test"
        };

        billCategoryRepository.Add(billCategory);
    }

    [TestMethod]
    public void BillCategoryUpdateTest()
    {
        var billCategory = new BillCategory
        {
            Id = "test",
            Name = "Updated Test"
        };

        billCategoryRepository.Update(billCategory);
    }

    [TestMethod]
    public void BillCategoryDeleteTest()
    {
        billCategoryRepository.Delete("test");
    }

    [TestMethod]
    public void BillCategoryGetByIdTest()
    {
        var billCategory = billCategoryRepository.GetById("test");
        if (billCategory != null)
        {
            Console.WriteLine(billCategory.Id);
            Console.WriteLine(billCategory.UserId);
            Console.WriteLine(billCategory.Name);
        }
    }

    [TestMethod]
    public void BillCategoryGetAllTest()
    {
        var list = billCategoryRepository.GetAll("testUser");
        foreach (var billCategory in list)
        {
            Console.WriteLine(billCategory.Id);
            Console.WriteLine(billCategory.UserId);
            Console.WriteLine(billCategory.Name);
        }
    }

    #endregion
}
