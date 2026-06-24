namespace InsurancePartnerManagement.Models
{
    public class PartnerPolicies
    {
        public Partner Partner { get; set; } = new Partner();
        public List<Policy> Policies { get; set; } = new List<Policy>();
    }
}
