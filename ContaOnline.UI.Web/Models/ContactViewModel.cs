using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using OnlineBill.Domain.Models;
using Org.BouncyCastle.Asn1.Mozilla;

namespace OnlineBill.UI.Web.Models
{
    public class ContactViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        [Required(ErrorMessage = "O nome deve ser informado.")]
        public string Name { get; set; }

        [Phone]
        public string? PrimaryTelephoneNumber { get; set; }

        [Phone]
        public string? SecondaryTelephoneNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O tipo deve ser informado.")]
        public JuristicNaturalPerson Type { get; set; }
        public string? CNPJ { get; set; }
        public string? CPF { get; set; }
        public string? RG { get; set; }
        public DateTime? BirthDate { get; set; }

        public Contact ConvertToContact(string loggedUserId)
        {
            Contact contact;

            if (Type == JuristicNaturalPerson.NaturalPerson)
            {
                contact = new Person();
                ((Person)contact).CPF = CPF;
                ((Person)contact).RG = RG;
                ((Person)contact).BirthDate = BirthDate;
            }
            else
            {
                contact = new Company();
                ((Company)contact).CNPJ = CNPJ;
            }

            contact.Id = Guid.NewGuid().ToString();
            contact.UserId = loggedUserId;
            contact.Name = Name;
            contact.PrimaryTelephoneNumber = PrimaryTelephoneNumber;
            contact.SecondaryTelephoneNumber = SecondaryTelephoneNumber;
            contact.Email = Email;
            contact.Type = Type;

            return contact;
        }
    }
}

