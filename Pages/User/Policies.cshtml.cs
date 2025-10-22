using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartInsuranceWeb.Models;
using SmartInsuranceWeb.Services;

namespace SmartInsuranceWeb.Pages.User
{
    public class PoliciesModel : PageModel
    {
        private readonly UserService _userService;
        private readonly PolicyService _policyService;

        public List<Policy> Policies { get; set; } = new();
        public Models.User? CurrentUser { get; set; }
        public string? FilterType { get; set; }
        public string? SortBy { get; set; }
        public string? SuccessMessage { get; set; }

        public PoliciesModel(UserService userService, PolicyService policyService)
        {
            _userService = userService;
            _policyService = policyService;
        }

        public IActionResult OnGet(string? filterType, string? sortBy)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToPage("/Login");

            CurrentUser = _userService.GetUserById(userId.Value);
            FilterType = filterType;
            SortBy = sortBy ?? "Name";

            Policies = string.IsNullOrEmpty(filterType)
                ? _policyService.GetActivePolicies()
                : _policyService.GetPoliciesByType(filterType);

            // Sorting
            Policies = SortBy switch
            {
                "Premium" => Policies.OrderBy(p => p.BasePremium).ToList(),
                "Coverage" => Policies.OrderByDescending(p => p.CoverageAmount).ToList(),
                _ => Policies.OrderBy(p => p.Name).ToList()
            };

            return Page();
        }

        public IActionResult OnPostBuy(int policyId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToPage("/Login");

            var user = _userService.GetUserById(userId.Value);
            var policy = _policyService.GetPolicyById(policyId);

            if (user != null && policy != null)
            {
                var premium = _policyService.CalculatePremium(policy, user);
                _policyService.PurchasePolicy(userId.Value, policyId, premium, policy.DurationMonths);
                TempData["SuccessMessage"] = $"Successfully purchased {policy.Name}!";
                return RedirectToPage("/User/Dashboard");
            }

            return Page();
        }

        public decimal CalculatePremium(Policy policy)
        {
            return CurrentUser != null ? _policyService.CalculatePremium(policy, CurrentUser) : policy.BasePremium;
        }
    }
}
