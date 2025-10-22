using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartInsuranceWeb.Models;
using SmartInsuranceWeb.Services;

namespace SmartInsuranceWeb.Pages.Admin
{
    public class ReportsModel : PageModel
    {
        private readonly UserService _userService;
        private readonly PolicyService _policyService;
        private readonly ClaimService _claimService;

        public decimal TotalRevenue { get; set; }
        public int TotalUsers { get; set; }
        public int TotalPurchases { get; set; }
        public int TotalClaims { get; set; }
        public int TotalPolicies { get; set; }
        public Dictionary<string, int> PolicyTypeDistribution { get; set; } = new();
        public Dictionary<string, (int Count, decimal Revenue)> PurchasesByType { get; set; } = new();
        public Dictionary<string, int> ClaimStats { get; set; } = new();
        public decimal PendingClaimAmount { get; set; }
        public decimal ApprovedClaimAmount { get; set; }
        public decimal RejectedClaimAmount { get; set; }
        public decimal TotalClaimAmount { get; set; }
        public List<Models.User> TopUsers { get; set; } = new();
        public List<UserPurchase> RecentPurchases { get; set; } = new();

        public ReportsModel(UserService userService, PolicyService policyService, ClaimService claimService)
        {
            _userService = userService;
            _policyService = policyService;
            _claimService = claimService;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("UserType") != "Admin")
                return RedirectToPage("/Login");

            // Basic stats
            var users = _userService.GetAllUsers();
            var policies = _policyService.GetAllPolicies();
            var purchases = _policyService.GetAllPurchases();
            var claims = _claimService.GetAllClaims();

            TotalUsers = users.Count;
            TotalPolicies = policies.Count;
            TotalPurchases = purchases.Count;
            TotalClaims = claims.Count;
            TotalRevenue = purchases.Sum(p => p.PremiumPaid);

            // Policy type distribution
            PolicyTypeDistribution = policies.GroupBy(p => p.Type)
                .ToDictionary(g => g.Key, g => g.Count());

            // Purchases by type
            PurchasesByType = purchases
                .Join(policies, p => p.PolicyId, pol => pol.Id, (p, pol) => new { Purchase = p, Policy = pol })
                .GroupBy(x => x.Policy.Type)
                .ToDictionary(
                    g => g.Key,
                    g => (Count: g.Count(), Revenue: g.Sum(x => x.Purchase.PremiumPaid))
                );

            // Claim statistics
            ClaimStats = _claimService.GetClaimStatistics();
            PendingClaimAmount = claims.Where(c => c.Status == "Pending").Sum(c => c.ClaimAmount);
            ApprovedClaimAmount = claims.Where(c => c.Status == "Approved").Sum(c => c.ClaimAmount);
            RejectedClaimAmount = claims.Where(c => c.Status == "Rejected").Sum(c => c.ClaimAmount);
            TotalClaimAmount = claims.Sum(c => c.ClaimAmount);

            // Top users
            TopUsers = users.OrderByDescending(u => u.PurchasedPolicyIds.Count).ToList();

            // Recent purchases
            RecentPurchases = purchases.OrderByDescending(p => p.PurchaseDate).ToList();

            return Page();
        }

        public string GetUserName(int userId)
        {
            var user = _userService.GetUserById(userId);
            return user?.Name ?? "Unknown";
        }

        public string GetPolicyName(int policyId)
        {
            var policy = _policyService.GetPolicyById(policyId);
            return policy?.Name ?? "Unknown";
        }
    }
}
