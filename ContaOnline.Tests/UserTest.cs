using OnlineBill.Domain.Interfaces;
using OnlineBill.Domain.Models;
using OnlineBill.Repository;

namespace OnlineBill.Tests;

[TestClass]
public class UserTest
{
    private UserRepository userRepository = new UserRepository();

    #region User Class Test

    [TestMethod]
    public void ValidateUserName()
    {
        var user = new User()
        {
            Id = "1",
            Email = "test@email.com",
            Password = "passwordTest"
        };

        var errors = user.Validate();
        Assert.AreEqual(1, errors.Count, $"The test returned {errors.Count} errors, but it should've returned 1 error.");
    }

    [TestMethod]
    public void ValidateUserPassword()
    {
        var user = new User()
        {
            Id = "1",
            Name = "Test",
            Email = "test@email.com"
        };

        var errors = user.Validate();
        Assert.AreEqual(1, errors.Count, $"The test returned {errors.Count} errors, but it should've returned 1 error.");
        Assert.AreEqual(errors[0], "Password must have at least 5 characters.", "Wrong message");
    }
    #endregion


    #region User Repository Test

    [TestMethod]
    public void UserAddTest()
    {
        var user = new User
        {
            Id = "test",
            Name = "Test",
            Email = "test@email.com",
            Password = "passwordTest"
        };
        userRepository.Add(user);
    }

    [TestMethod]
    public void UserUpdateTest()
    {
        var userData = new User
        {
            Id = "Test",
            Name = "Updated Test",
            Email = "testUpdated@email.com",
            Password = "passwordTestUpdated"
        };
        userRepository.Update(userData);
    }

    [TestMethod]
    public void UserDeleteTest()
    {
        userRepository.Delete("test");
    }

    [TestMethod]
    public void UserGetByIdTest()
    {
        var user = userRepository.GetById("test");
        if (user != null)
        {
            Console.WriteLine(user.Id);
            Console.WriteLine(user.Name);
            Console.WriteLine(user.Email);
            Console.WriteLine(user.Password);
        }
    }

    [TestMethod]
    public void UserGetByEmailPasswordTest()
    {
        string userEmail = "test@email.com";
        string userPassword = "passwordTest";

        var user = userRepository.GetByEmailPassword(userEmail, userPassword);
        if (user != null )
        {
            Console.WriteLine(user.Id);
            Console.WriteLine(user.Name);
            Console.WriteLine(user.Email);
            Console.WriteLine(user.Password);
        }
    }

    [TestMethod]
    public void UserGetAllTest()
    {
        var userList = userRepository.GetAll();
        foreach (var user in userList)
        {
            Console.WriteLine(user.Name);
            Console.WriteLine(user.Email);
        }
    }
    #endregion
}
