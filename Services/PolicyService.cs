using SmartInsuranceWeb.Models;

namespace SmartInsuranceWeb.Services
{
    public class PolicyService
    {
        private readonly DataService _dataService;
        private const string PoliciesFile = "policies.json";
        private const string PurchasesFile = "purchases.json";

        public PolicyService(DataService dataService)
        {
            _dataService = dataService;
        }

        public List<Policy> GetAllPolicies() => _dataService.ReadJson<Policy>(PoliciesFile);

        public List<Policy> GetActivePolicies() => GetAllPolicies().Where(p => p.IsActive).ToList();

        public Policy? GetPolicyById(int id) => GetAllPolicies().FirstOrDefault(p => p.Id == id);

        public List<Policy> GetPoliciesByType(string type) => GetActivePolicies().Where(p => p.Type.Equals(type, StringComparison.OrdinalIgnoreCase)).ToList();

        public void AddPolicy(Policy policy)
        {
            var policies = GetAllPolicies();
            policy.Id = policies.Any() ? policies.Max(p => p.Id) + 1 : 1;
            policies.Add(policy);
            _dataService.WriteJson(PoliciesFile, policies);
        }

        public void UpdatePolicy(Policy policy)
        {
            var policies = GetAllPolicies();
            var index = policies.FindIndex(p => p.Id == policy.Id);
            if (index >= 0)
            {
                policies[index] = policy;
                _dataService.WriteJson(PoliciesFile, policies);
            }
        }

        public bool DeletePolicy(int id)
        {
            var policies = GetAllPolicies();
            var policy = policies.FirstOrDefault(p => p.Id == id);
            if (policy != null)
            {
                policies.Remove(policy);
                _dataService.WriteJson(PoliciesFile, policies);
                return true;
            }
            return false;
        }

        // Purchase methods
        public List<UserPurchase> GetAllPurchases() => _dataService.ReadJson<UserPurchase>(PurchasesFile);

        public List<UserPurchase> GetUserPurchases(int userId) => GetAllPurchases().Where(p => p.UserId == userId && p.IsActive).ToList();

        public void PurchasePolicy(int userId, int policyId, decimal premium, int durationMonths)
        {
            var purchases = GetAllPurchases();
            var purchase = new UserPurchase
            {
                Id = purchases.Any() ? purchases.Max(p => p.Id) + 1 : 1,
                UserId = userId,
                PolicyId = policyId,
                PremiumPaid = premium,
                PurchaseDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddMonths(durationMonths)
            };
            purchases.Add(purchase);
            _dataService.WriteJson(PurchasesFile, purchases);
        }

        public decimal CalculatePremium(Policy policy, User user)
        {
            decimal premium = policy.BasePremium;

            // Age-based risk factor
            if (user.Age < 25)
                premium *= 1.2m;
            else if (user.Age > 60)
                premium *= 1.5m;

            // Gender-based (example logic)
            if (policy.Type == "Life" && user.Gender == "Male")
                premium *= 1.1m;

            return Math.Round(premium, 2);
        }

        public List<Policy> GetRecommendedPolicies(User user)
        {
            var policies = GetActivePolicies();
            var recommendations = new List<Policy>();

            // Age-based recommendations
            if (user.Age < 30)
            {
                recommendations.AddRange(policies.Where(p => p.Type == "Health" || p.Type == "Life"));
            }
            else if (user.Age >= 30 && user.Age < 50)
            {
                recommendations.AddRange(policies.Where(p => p.Type == "Auto" || p.Type == "Home"));
            }
            else
            {
                recommendations.AddRange(policies.Where(p => p.Type == "Health" || p.Type == "Life"));
            }

            return recommendations.Take(5).ToList();
        }
    }
}
