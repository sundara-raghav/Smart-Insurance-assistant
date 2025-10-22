using SmartInsuranceWeb.Models;

namespace SmartInsuranceWeb.Services
{
    public class UserService
    {
        private readonly DataService _dataService;
        private const string UsersFile = "users.json";
        private const string AdminsFile = "admins.json";

        public UserService(DataService dataService)
        {
            _dataService = dataService;
        }

        public List<User> GetAllUsers() => _dataService.ReadJson<User>(UsersFile);

        public User? GetUserById(int id) => GetAllUsers().FirstOrDefault(u => u.Id == id);

        public User? GetUserByEmail(string email) => GetAllUsers().FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        public bool RegisterUser(User user)
        {
            var users = GetAllUsers();
            if (users.Any(u => u.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase)))
                return false;

            user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
            users.Add(user);
            _dataService.WriteJson(UsersFile, users);
            return true;
        }

        public void UpdateUser(User user)
        {
            var users = GetAllUsers();
            var index = users.FindIndex(u => u.Id == user.Id);
            if (index >= 0)
            {
                users[index] = user;
                _dataService.WriteJson(UsersFile, users);
            }
        }

        public bool DeleteUser(int id)
        {
            var users = GetAllUsers();
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                users.Remove(user);
                _dataService.WriteJson(UsersFile, users);
                return true;
            }
            return false;
        }

        public bool ValidateUser(string email, string password)
        {
            var user = GetUserByEmail(email);
            return user != null && user.Password == password;
        }

        // Admin methods
        public List<Admin> GetAllAdmins() => _dataService.ReadJson<Admin>(AdminsFile);

        public Admin? GetAdminByEmail(string email) => GetAllAdmins().FirstOrDefault(a => a.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        public bool ValidateAdmin(string email, string password)
        {
            var admin = GetAdminByEmail(email);
            return admin != null && admin.Password == password;
        }
    }
}
