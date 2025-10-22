using SmartInsuranceWeb.Models;

namespace SmartInsuranceWeb.Services
{
    public class ClaimService
    {
        private readonly DataService _dataService;
        private const string ClaimsFile = "claims.json";

        public ClaimService(DataService dataService)
        {
            _dataService = dataService;
        }

        public List<Claim> GetAllClaims() => _dataService.ReadJson<Claim>(ClaimsFile);

        public List<Claim> GetUserClaims(int userId) => GetAllClaims().Where(c => c.UserId == userId).OrderByDescending(c => c.FiledDate).ToList();

        public List<Claim> GetPendingClaims() => GetAllClaims().Where(c => c.Status == "Pending").OrderByDescending(c => c.FiledDate).ToList();

        public Claim? GetClaimById(int id) => GetAllClaims().FirstOrDefault(c => c.Id == id);

        public void FileClaim(Claim claim)
        {
            var claims = GetAllClaims();
            claim.Id = claims.Any() ? claims.Max(c => c.Id) + 1 : 1;
            claims.Add(claim);
            _dataService.WriteJson(ClaimsFile, claims);
        }

        public void UpdateClaimStatus(int id, string status, string? adminNotes = null)
        {
            var claims = GetAllClaims();
            var claim = claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = status;
                claim.ProcessedDate = DateTime.Now;
                claim.AdminNotes = adminNotes;
                _dataService.WriteJson(ClaimsFile, claims);
            }
        }

        public Dictionary<string, int> GetClaimStatistics()
        {
            var claims = GetAllClaims();
            return new Dictionary<string, int>
            {
                ["Total"] = claims.Count,
                ["Pending"] = claims.Count(c => c.Status == "Pending"),
                ["Approved"] = claims.Count(c => c.Status == "Approved"),
                ["Rejected"] = claims.Count(c => c.Status == "Rejected")
            };
        }
    }
}
