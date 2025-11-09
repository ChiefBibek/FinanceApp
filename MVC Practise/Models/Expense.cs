using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Models
{
    public class Expense
    {
        public int ID { get; set; }
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        [Range(0.01,double.MaxValue,ErrorMessage = "Amount needs to be higher than 0")]
        public decimal Amount { get; set; }
        [Required]
        public String Category { get; set; } = null!;

        public DateTime Date { get; set; } = DateTime.Now;

    }
}
