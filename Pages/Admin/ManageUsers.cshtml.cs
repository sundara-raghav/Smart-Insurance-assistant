using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartInsuranceWeb.Models;
using SmartInsuranceWeb.Services;

namespace SmartInsuranceWeb.Pages.Admin
{
    public class ManageUsersModel : PageModel
    {
        private readonly UserService _userService;
        private readonly PolicyService _policyService;

        public List<Models.User> Users { get; set; } = new();
        public string? SuccessMessage { get; set; }

        public ManageUsersModel(UserService userService, PolicyService policyService)
        {
            _userService = userService;
            _policyService = policyService;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("UserType") != "Admin")
                return RedirectToPage("/Login");

            Users = _userService.GetAllUsers();
            return Page();
        }

        public IActionResult OnPostDelete(int userId)
        {
            if (_userService.DeleteUser(userId))
            {
                TempData["SuccessMessage"] = "User deleted successfully!";
            }
            return RedirectToPage();
        }

        public Policy? GetPolicy(int policyId)
        {
            return _policyService.GetPolicyById(policyId);
        }
    }
}
