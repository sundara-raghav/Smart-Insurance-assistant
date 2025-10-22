using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartInsuranceWeb.Services;

namespace SmartInsuranceWeb.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserService _userService;

        public string? ErrorMessage { get; set; }

        public LoginModel(UserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(string email, string password, string userType)
        {
            if (userType == "Admin")
            {
                if (_userService.ValidateAdmin(email, password))
                {
                    var admin = _userService.GetAdminByEmail(email);
                    HttpContext.Session.SetString("UserType", "Admin");
                    HttpContext.Session.SetInt32("UserId", admin!.Id);
                    HttpContext.Session.SetString("UserName", admin.Name);
                    return RedirectToPage("/Admin/Dashboard");
                }
            }
            else
            {
                if (_userService.ValidateUser(email, password))
                {
                    var user = _userService.GetUserByEmail(email);
                    HttpContext.Session.SetString("UserType", "User");
                    HttpContext.Session.SetInt32("UserId", user!.Id);
                    HttpContext.Session.SetString("UserName", user.Name);
                    return RedirectToPage("/User/Dashboard");
                }
            }

            ErrorMessage = "Invalid email or password";
            return Page();
        }
    }
}
