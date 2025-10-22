namespace SmartInsuranceWeb.Models
{
    public class Policy
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // Health, Life, Auto, Home, Travel
        public string Description { get; set; } = string.Empty;
        public decimal BasePremium { get; set; }
        public decimal CoverageAmount { get; set; }
        public int DurationMonths { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<string> Features { get; set; } = new List<string>();
    }
}
