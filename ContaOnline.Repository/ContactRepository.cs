using System;
using Google.Protobuf.Compiler;
using OnlineBill.Domain.Interfaces;
using OnlineBill.Domain.Models;

namespace OnlineBill.Repository
{
    public class ContactRepository : IContactRepository
    {
        public void Add(Contact contact)
        {
            string storedProcedure;

            if (contact.Type == JuristicNaturalPerson.NaturalPerson)
            {
                storedProcedure = "spr_contact_person_add";
            }
            else if (contact.Type == JuristicNaturalPerson.JuristicPerson) 
            {
                storedProcedure = "spr_contact_company_add";
            }
            else
            {
                throw new Exception("Ocorreu um erro ao editar o contato.");
            }

            Database.Execute(storedProcedure, contact);
        }

        public void Update(Contact contact)
        {
            string storedProcedure;
            dynamic parameters = new {};

            if (contact.Type == JuristicNaturalPerson.NaturalPerson)
            {
                storedProcedure = "spr_contact_person_update";

                var person = contact as Person;

                parameters = new
                {
                    id = contact.Id,
                    name = contact.Name,
                    primaryTelephoneNumber = contact.PrimaryTelephoneNumber,
                    secondaryTelephoneNumber = contact.SecondaryTelephoneNumber,
                    email = contact.Email,
                    type = contact.Type,
                    cpf = person?.CPF,
                    rg = person?.RG,
                    birthDate = person?.BirthDate
                };
            }
            else if (contact.Type == JuristicNaturalPerson.JuristicPerson)
            {
                storedProcedure = "spr_contact_company_update";

                var company = contact as Company;

                parameters = new
                {
                    id = contact.Id,
                    name = contact.Name,
                    primaryTelephoneNumber = contact.PrimaryTelephoneNumber,
                    secondaryTelephoneNumber = contact.SecondaryTelephoneNumber,
                    email = contact.Email,
                    type = contact.Type,
                    cnpj = company?.CNPJ
                };
            }
            else
            {
                throw new Exception("Ocorreu um erro ao editar o contato.");
            }

            Database.Execute(storedProcedure, parameters);
        }

        public void Delete(string id)
        {
            string storedProcedure = "spr_contact_delete";

            Database.Execute(storedProcedure, new { id = id });
        }

        public IEnumerable<Contact> GetAll(string userId)
        {
            string storedProcedure = "spr_contact_get_all";

            return Database.QueryCollection<Contact>(storedProcedure, new { userId = userId });
        }

        public Contact GetById(string id)
        {
            string storedProcedure = "spr_contact_get_by_id";

            var contact = Database.QueryEntity<Contact>(storedProcedure, new { id = id });

            if (contact.Type == JuristicNaturalPerson.NaturalPerson)
            {
                contact = Database.QueryEntity<Person>(storedProcedure, new { id = id }); ;
            }
            else if (contact.Type == JuristicNaturalPerson.JuristicPerson)
            {
                contact = Database.QueryEntity<Company>(storedProcedure, new { id = id });
            }

            return contact;
        }

        public IEnumerable<string> Validate()
        {
            throw new NotImplementedException();
        }
    }
}
