using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineBill.Domain.Models
{
    public class BillCategory
    {
        public string Id { get; set; }
        public string UserId { get; set; }

        [Required(ErrorMessage = "O nome deve ser informado.")]
        public string Name { get; set; }
    }
}
