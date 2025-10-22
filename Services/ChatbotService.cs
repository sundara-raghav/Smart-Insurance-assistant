using System.Text;
using System.Text.Json;

namespace SmartInsuranceWeb.Services
{
    public class ChatbotService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly ILogger<ChatbotService> _logger;

        public ChatbotService(IConfiguration configuration, ILogger<ChatbotService> logger)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["GeminiAI:ApiKey"] ?? "";
            _logger = logger;
        }

        public async Task<string> GetChatResponseAsync(string userMessage, int? userId = null, List<string>? conversationHistory = null)
        {
            try
            {
                // Check if API key is configured and valid
                if (string.IsNullOrEmpty(_apiKey) || _apiKey.Length < 20)
                {
                    return GetFallbackResponse(userMessage);
                }

                // Simple system prompt
                var systemPrompt = @"You are a helpful AI assistant for Smart Insurance Assistant. 
Help users with insurance questions about Health, Life, Auto, Home, and Travel insurance.
Keep responses clear, friendly, and under 3 paragraphs.";

                // Build the request
                var fullPrompt = systemPrompt + "\n\nUser: " + userMessage + "\n\nAssistant:";
                
                var requestBody = new
                {
                    contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new { text = fullPrompt }
                            }
                        }
                    }
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Call Gemini API - using gemini-1.5-flash (stable release)
                var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={_apiKey}";
                
                _logger.LogInformation("Calling Gemini API...");
                var response = await _httpClient.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"API Error: {response.StatusCode} - {errorBody}");
                    return GetFallbackResponse(userMessage);
                }

                // Parse response
                var responseText = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"API Response: {responseText.Substring(0, Math.Min(200, responseText.Length))}...");
                
                var result = JsonSerializer.Deserialize<JsonElement>(responseText);

                if (result.TryGetProperty("candidates", out var candidates) && candidates.GetArrayLength() > 0)
                {
                    var candidate = candidates[0];
                    if (candidate.TryGetProperty("content", out var contentObj) &&
                        contentObj.TryGetProperty("parts", out var parts) && parts.GetArrayLength() > 0)
                    {
                        var part = parts[0];
                        if (part.TryGetProperty("text", out var textElement))
                        {
                            return textElement.GetString() ?? "Hello! How can I help you today?";
                        }
                    }
                }

                return GetFallbackResponse(userMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Chatbot Error: {ex.Message}");
                return GetFallbackResponse(userMessage);
            }
        }

        private string GetFallbackResponse(string userMessage)
        {
            var message = userMessage.ToLower();
            
            if (message.Contains("health") || message.Contains("medical"))
            {
                return "Our Health Insurance policies provide comprehensive medical coverage including hospitalization, outpatient care, and preventive services. Would you like to know more about specific health plans or coverage options?";
            }
            
            if (message.Contains("life") || message.Contains("death"))
            {
                return "Life Insurance provides financial security for your loved ones. We offer term life, whole life, and universal life policies with various coverage amounts. What type of life insurance are you interested in?";
            }
            
            if (message.Contains("auto") || message.Contains("car") || message.Contains("vehicle"))
            {
                return "Auto Insurance protects you and your vehicle with liability, collision, and comprehensive coverage. We offer competitive rates and various deductible options. Do you need a quote for your vehicle?";
            }
            
            if (message.Contains("home") || message.Contains("house") || message.Contains("property"))
            {
                return "Home Insurance protects your property and belongings from damage, theft, and liability. Coverage includes dwelling, personal property, and additional living expenses. What specific coverage are you looking for?";
            }
            
            if (message.Contains("travel") || message.Contains("trip"))
            {
                return "Travel Insurance covers trip cancellation, medical emergencies abroad, lost luggage, and travel delays. Perfect for domestic and international trips. Are you planning a specific trip?";
            }
            
            if (message.Contains("claim") || message.Contains("file"))
            {
                return "To file a claim, you can use our online portal or call our 24/7 claims hotline. You'll need your policy number and details about the incident. Would you like help with the claims process?";
            }
            
            if (message.Contains("premium") || message.Contains("cost") || message.Contains("price"))
            {
                return "Insurance premiums depend on various factors including coverage type, amount, your age, and risk factors. I can help you understand how premiums are calculated or connect you with an agent for a personalized quote.";
            }
            
            if (message.Contains("hello") || message.Contains("hi") || message.Contains("hey"))
            {
                return "Hello! I'm your Smart Insurance Assistant. I can help you with information about our Health, Life, Auto, Home, and Travel insurance policies. What would you like to know?";
            }
            
            return "I'm here to help with all your insurance questions! I can provide information about our Health, Life, Auto, Home, and Travel insurance policies, help with claims, explain coverage options, and more. What specific insurance topic can I assist you with today?";
        }
    }
}
