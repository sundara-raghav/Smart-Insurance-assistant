using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartInsuranceWeb.Models;
using SmartInsuranceWeb.Services;

namespace SmartInsuranceWeb.Pages.User
{
    public class DashboardModel : PageModel
    {
        private readonly UserService _userService;
        private readonly PolicyService _policyService;
        private readonly ClaimService _claimService;

        public Models.User? CurrentUser { get; set; }
        public List<UserPurchase> PurchasedPolicies { get; set; } = new();
        public List<Claim> MyClaims { get; set; } = new();
        public List<Policy> RecommendedPolicies { get; set; } = new();

        public DashboardModel(UserService userService, PolicyService policyService, ClaimService claimService)
        {
            _userService = userService;
            _policyService = policyService;
            _claimService = claimService;
        }

        public IActionResult OnGet()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToPage("/Login");

            CurrentUser = _userService.GetUserById(userId.Value);
            if (CurrentUser == null)
                return RedirectToPage("/Login");

            PurchasedPolicies = _policyService.GetUserPurchases(userId.Value);
            MyClaims = _claimService.GetUserClaims(userId.Value);
            RecommendedPolicies = _policyService.GetRecommendedPolicies(CurrentUser);

            return Page();
        }
    }
}
