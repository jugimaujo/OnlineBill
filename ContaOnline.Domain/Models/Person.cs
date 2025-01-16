using System;

namespace OnlineBill.Domain.Models
{
    public class Person : Contact
    {
        public string CPF {  get; set; }
        public string RG {  get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
