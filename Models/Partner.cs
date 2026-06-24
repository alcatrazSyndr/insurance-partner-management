using System.ComponentModel.DataAnnotations;

namespace InsurancePartnerManagement.Models
{
    public class Partner
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ime je obavezno")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Ime mora imati između 2 i 255 znakova")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Prezime je obavezno")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "Prezime mora imati između 2 i 255 znakova")]
        public string LastName { get; set; } = string.Empty;

        public string? Address { get; set; }

        [Required(ErrorMessage = "Broj partnera je obavezan")]
        [RegularExpression(@"^\d{20}$", ErrorMessage = "Broj partnera mora sadržavati točno 20 znamenki")]
        public string PartnerNumber { get; set; } = string.Empty;

        [RegularExpression(@"^\d{11}$", ErrorMessage = "OIB mora sadržavati točno 11 znamenki")]
        public string? CroatianPIN { get; set; }

        [Required(ErrorMessage = "Tip partnera je obavezan")]
        [Range(1, 2, ErrorMessage = "Tip partnera mora biti Personal ili Legal")]
        public int PartnerTypeId { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        [Required(ErrorMessage = "Email korisnika je obavezan")]
        [EmailAddress(ErrorMessage = "Neispravan format email adrese")]
        [StringLength(255, ErrorMessage = "Email ne smije biti duži od 255 znakova")]
        public string CreatedByUser { get; set; } = string.Empty;

        [Required(ErrorMessage = "Polje je obavezno")]
        public bool IsForeign { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]{10,20}$", ErrorMessage = "ExternalCode mora biti alfanumerik između 10 i 20 znakova")]
        public string? ExternalCode { get; set; }

        [Required(ErrorMessage = "Spol je obavezan")]
        [RegularExpression(@"^[MFN]$", ErrorMessage = "Spol mora biti M, F ili N")]
        public string Gender { get; set; } = string.Empty;
    }
}