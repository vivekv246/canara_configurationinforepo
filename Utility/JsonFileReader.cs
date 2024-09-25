using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConfigurationInfo.Utility
{
    public static class JsonFileReader
    {
        public static T Read<T>(string filePath)
        {
            string text = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(text);
        }
    }
}
