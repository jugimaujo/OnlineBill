using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineBill.Domain.Models
{
    public class CheckingAccount : BaseDomain
    {
        [Required(ErrorMessage = "A descrição deve ser informada.")]
        public string Description { get; set; }
    }
}
