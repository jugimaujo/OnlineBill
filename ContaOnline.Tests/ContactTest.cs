using System;
using OnlineBill.Repository;
using OnlineBill.Domain.Models;

namespace OnlineBill.Tests;

[TestClass]
public class ContactTest
{
    ContactRepository contactRepository = new ContactRepository();

    #region Contact Repository Test

    [TestMethod]
    public void ContactAddTest()
    {
        var contact = new Contact
        {
            Id = "test",
            UserId = "test",
            Name = "Test Contact",
            PrimaryTelephoneNumber = "(00) 00000-0000",
            SecondaryTelephoneNumber = "+11 (11) 11111-1111",
            Email = "test@email.com",
            Type = JuristicNaturalPerson.JuristicPerson
        };

        contactRepository.Add(contact);
    }

    [TestMethod]
    public void ContactGetAllTest()
    {
        var list = contactRepository.GetAll("test");
        Console.WriteLine(list);
    }

    #endregion
}
