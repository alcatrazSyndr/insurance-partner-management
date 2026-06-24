using System.ComponentModel.DataAnnotations;

namespace InsurancePartnerManagement.Models
{
    public class Policy
    {
        public int Id { get; set; }

        [Required]
        public int PartnerId { get; set; }

        [Required(ErrorMessage = "Broj police je obavezan")]
        [RegularExpression(@"^[a-zA-Z0-9]{10,15}$", ErrorMessage = "Broj police mora biti alfanumerik između 10 i 15 znakova")]
        public string PolicyNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Iznos police je obavezan")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Iznos mora biti veći od 0")]
        public decimal Amount { get; set; }

        public DateTime CreatedAtUtc { get; set; }
    }
}