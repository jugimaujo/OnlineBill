using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineBill.Domain.Models
{
    public class CheckingAccount
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessage = "A descrição deve ser informada.")]
        public string Description { get; set; }
    }
}
