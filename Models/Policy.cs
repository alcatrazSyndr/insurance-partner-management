namespace InsurancePartnerManagement.Models
{
    public class Policy
    {
        public int Id { get; set; }
        public int PartnerId { get; set; }
        public string PolicyNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime CreatedAtUtc { get; set; }
    }
}
