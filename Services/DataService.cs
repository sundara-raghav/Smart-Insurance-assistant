using System.Text.Json;
using SmartInsuranceWeb.Models;

namespace SmartInsuranceWeb.Services
{
    public class DataService
    {
        private readonly string _dataPath;
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions 
        { 
            WriteIndented = true 
        };

        public DataService(IWebHostEnvironment env)
        {
            _dataPath = Path.Combine(env.WebRootPath, "data");
            Directory.CreateDirectory(_dataPath);
        }

        private string GetFilePath(string filename) => Path.Combine(_dataPath, filename);

        public List<T> ReadJson<T>(string filename)
        {
            try
            {
                var path = GetFilePath(filename);
                if (!File.Exists(path))
                    return new List<T>();

                var json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
            catch
            {
                return new List<T>();
            }
        }

        public void WriteJson<T>(string filename, List<T> data)
        {
            var path = GetFilePath(filename);
            var json = JsonSerializer.Serialize(data, _jsonOptions);
            File.WriteAllText(path, json);
        }
    }
}
