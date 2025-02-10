using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBill.Domain.Models
{
    public class Bill
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessage = "Uma conta corrente deve ser informada.")]
        public string CheckingAccountId { get; set; }
        public PayReceive Type { get; set; }
        [Required(ErrorMessage = "A categoria deve ser informada.")]
        public string CategoryId { get; set; }
        public string? ContactId { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "A data de vencimento deve ser informada.")]
        public DateTime DueDate { get; set; }
        [Required(ErrorMessage = "Deve ter um valor de pagamento.")]
        public decimal Value { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Increase {  get; set; }
        public decimal? PaidValue { get; set; }
    }
}
