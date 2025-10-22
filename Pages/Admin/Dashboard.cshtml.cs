using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartInsuranceWeb.Models;
using SmartInsuranceWeb.Services;

namespace SmartInsuranceWeb.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly UserService _userService;
        private readonly PolicyService _policyService;
        private readonly ClaimService _claimService;

        public int TotalUsers { get; set; }
        public int TotalPolicies { get; set; }
        public int PendingClaims { get; set; }
        public int TotalPurchases { get; set; }
        public Dictionary<string, int> ClaimStats { get; set; } = new();
        public List<Claim> RecentPendingClaims { get; set; } = new();

        public DashboardModel(UserService userService, PolicyService policyService, ClaimService claimService)
        {
            _userService = userService;
            _policyService = policyService;
            _claimService = claimService;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("UserType") != "Admin")
                return RedirectToPage("/Login");

            TotalUsers = _userService.GetAllUsers().Count;
            TotalPolicies = _policyService.GetAllPolicies().Count;
            TotalPurchases = _policyService.GetAllPurchases().Count;
            ClaimStats = _claimService.GetClaimStatistics();
            PendingClaims = ClaimStats.GetValueOrDefault("Pending", 0);
            RecentPendingClaims = _claimService.GetPendingClaims();

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
