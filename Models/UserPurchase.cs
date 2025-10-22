namespace SmartInsuranceWeb.Models
{
    public class UserPurchase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PolicyId { get; set; }
        public decimal PremiumPaid { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
