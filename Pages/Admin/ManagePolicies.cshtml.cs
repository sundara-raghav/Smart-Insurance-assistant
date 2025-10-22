using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartInsuranceWeb.Models;
using SmartInsuranceWeb.Services;

namespace SmartInsuranceWeb.Pages.Admin
{
    public class ManagePoliciesModel : PageModel
    {
        private readonly PolicyService _policyService;

        public List<Policy> Policies { get; set; } = new();
        public string? SuccessMessage { get; set; }

        public ManagePoliciesModel(PolicyService policyService)
        {
            _policyService = policyService;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("UserType") != "Admin")
                return RedirectToPage("/Login");

            Policies = _policyService.GetAllPolicies();
            return Page();
        }

        public IActionResult OnPostAdd(string name, string type, string description, decimal basePremium, 
            decimal coverageAmount, int durationMonths, int minAge, int maxAge)
        {
            var policy = new Policy
            {
                Name = name,
                Type = type,
                Description = description,
                BasePremium = basePremium,
                CoverageAmount = coverageAmount,
                DurationMonths = durationMonths,
                MinAge = minAge,
                MaxAge = maxAge,
                Features = new List<string>()
            };

            _policyService.AddPolicy(policy);
            TempData["SuccessMessage"] = "Policy added successfully!";
            return RedirectToPage();
        }

        public IActionResult OnPostUpdate(int policyId, string name, string type, string description, 
            decimal basePremium, decimal coverageAmount, int durationMonths, int minAge, int maxAge, bool isActive)
        {
            var policy = _policyService.GetPolicyById(policyId);
            if (policy != null)
            {
                policy.Name = name;
                policy.Type = type;
                policy.Description = description;
                policy.BasePremium = basePremium;
                policy.CoverageAmount = coverageAmount;
                policy.DurationMonths = durationMonths;
                policy.MinAge = minAge;
                policy.MaxAge = maxAge;
                policy.IsActive = isActive;

                _policyService.UpdatePolicy(policy);
                TempData["SuccessMessage"] = "Policy updated successfully!";
            }
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int policyId)
        {
            _policyService.DeletePolicy(policyId);
            TempData["SuccessMessage"] = "Policy deleted successfully!";
            return RedirectToPage();
        }
    }
}
