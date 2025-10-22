using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartInsuranceWeb.Services;
using System.Text.Json;

namespace SmartInsuranceWeb.Pages
{
    [IgnoreAntiforgeryToken]
    public class ChatModel : PageModel
    {
        private readonly ChatbotService _chatbotService;

        public ChatModel(ChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        public void OnGet()
        {
            // This page is just for API endpoint
        }

        public async Task<IActionResult> OnPostAsync([FromBody] ChatRequest request)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                var response = await _chatbotService.GetChatResponseAsync(
                    request.Message, 
                    userId, 
                    request.History
                );

                return new JsonResult(new { response = response, success = true });
            }
            catch
            {
                return new JsonResult(new { response = "I'm experiencing technical difficulties. Please try again.", success = false });
            }
        }
    }

    public class ChatRequest
    {
        public string Message { get; set; } = "";
        public List<string>? History { get; set; }
    }
}
