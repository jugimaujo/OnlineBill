using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace OnlineBill.Domain.Models
{
    public class Contact
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string? PrimaryTelephoneNumber { get; set; }
        public string? SecondaryTelephoneNumber { get; set; }
        public string? Email { get; set; }
        public JuristicNaturalPerson Type { get; set; }
    }
}
