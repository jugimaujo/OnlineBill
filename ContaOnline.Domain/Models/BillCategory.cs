using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineBill.Domain.Models
{
    public class BillCategory : BaseDomain
    {
        [Required(ErrorMessage = "O nome deve ser informado.")]
        public string Name { get; set; }
    }
}
