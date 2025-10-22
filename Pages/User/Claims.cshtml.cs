using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartInsuranceWeb.Models;
using SmartInsuranceWeb.Services;

namespace SmartInsuranceWeb.Pages.User
{
    public class ClaimsModel : PageModel
    {
        private readonly UserService _userService;
        private readonly PolicyService _policyService;
        private readonly ClaimService _claimService;

        public List<Claim> MyClaims { get; set; } = new();
        public List<UserPurchase> UserPurchases { get; set; } = new();
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public ClaimsModel(UserService userService, PolicyService policyService, ClaimService claimService)
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

            MyClaims = _claimService.GetUserClaims(userId.Value);
            UserPurchases = _policyService.GetUserPurchases(userId.Value);

            return Page();
        }

        public IActionResult OnPost(int policyId, string claimType, decimal claimAmount, string description)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToPage("/Login");

            var claim = new Claim
            {
                UserId = userId.Value,
                PolicyId = policyId,
                ClaimType = claimType,
                ClaimAmount = claimAmount,
                Description = description
            };

            _claimService.FileClaim(claim);
            SuccessMessage = "Claim filed successfully! It will be reviewed by our team.";

            MyClaims = _claimService.GetUserClaims(userId.Value);
            UserPurchases = _policyService.GetUserPurchases(userId.Value);

            return Page();
        }

        public string GetPolicyName(int policyId)
        {
            var policy = _policyService.GetPolicyById(policyId);
            return policy?.Name ?? "Unknown Policy";
        }

        public Policy? GetPolicy(int policyId)
        {
            return _policyService.GetPolicyById(policyId);
        }
    }
}
