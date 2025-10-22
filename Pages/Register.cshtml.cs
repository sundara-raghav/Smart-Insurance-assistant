using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartInsuranceWeb.Models;
using SmartInsuranceWeb.Services;

namespace SmartInsuranceWeb.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserService _userService;

        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public RegisterModel(UserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(string name, string email, string password, string phone, int age, string gender, string address)
        {
            var user = new Models.User
            {
                Name = name,
                Email = email,
                Password = password,
                Phone = phone,
                Age = age,
                Gender = gender,
                Address = address
            };

            if (_userService.RegisterUser(user))
            {
                SuccessMessage = "Registration successful! You can now login.";
                return Page();
            }

            ErrorMessage = "Email already exists. Please use a different email.";
            return Page();
        }
    }
}
