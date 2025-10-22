using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartInsuranceWeb.Models;
using SmartInsuranceWeb.Services;

namespace SmartInsuranceWeb.Pages.Admin
{
    public class ManageClaimsModel : PageModel
    {
        private readonly UserService _userService;
        private readonly PolicyService _policyService;
        private readonly ClaimService _claimService;

        public List<Claim> Claims { get; set; } = new();
        public string? SuccessMessage { get; set; }
        public string Filter { get; set; } = "Pending";

        public ManageClaimsModel(UserService userService, PolicyService policyService, ClaimService claimService)
        {
            _userService = userService;
            _policyService = policyService;
            _claimService = claimService;
        }

        public IActionResult OnGet(string? filter)
        {
            if (HttpContext.Session.GetString("UserType") != "Admin")
                return RedirectToPage("/Login");

            Filter = filter ?? "Pending";

            Claims = Filter switch
            {
                "Pending" => _claimService.GetPendingClaims(),
                "Approved" => _claimService.GetAllClaims().Where(c => c.Status == "Approved").OrderByDescending(c => c.ProcessedDate).ToList(),
                "Rejected" => _claimService.GetAllClaims().Where(c => c.Status == "Rejected").OrderByDescending(c => c.ProcessedDate).ToList(),
                _ => _claimService.GetAllClaims().OrderByDescending(c => c.FiledDate).ToList()
            };

            return Page();
        }

        public IActionResult OnPost(int claimId, string action, string? adminNotes)
        {
            if (action == "Approve")
            {
                _claimService.UpdateClaimStatus(claimId, "Approved", adminNotes);
                TempData["SuccessMessage"] = "Claim approved successfully!";
            }
            else if (action == "Reject")
            {
                _claimService.UpdateClaimStatus(claimId, "Rejected", adminNotes);
                TempData["SuccessMessage"] = "Claim rejected successfully!";
            }

            return RedirectToPage();
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
