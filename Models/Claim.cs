namespace SmartInsuranceWeb.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PolicyId { get; set; }
        public string ClaimType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal ClaimAmount { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected
        public DateTime FiledDate { get; set; } = DateTime.Now;
        public DateTime? ProcessedDate { get; set; }
        public string? AdminNotes { get; set; }
    }
}
