using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DownloadFinancialReports
{
    public static class HttpClientExtention
    {
        const string hashString = ".cache";
        static SHA256 algorithm = SHA256.Create();
        static HttpClient client = new HttpClient();

        public static async Task<byte[]> GetBytesAsync(this string url, int delay = 0)
        {
            var cacheKey = string.Join("", algorithm.ComputeHash(Encoding.UTF8.GetBytes(url)).Select(b => b.ToString("X2")));
            var cacheFilePath = Path.Join(hashString, cacheKey);

            if (!Directory.Exists(hashString))
                Directory.CreateDirectory(hashString);

            byte[] bytes;
            if (File.Exists(cacheFilePath))
            {
                bytes = await File.ReadAllBytesAsync(cacheFilePath);
            }
            else
            {
                bytes = await client.GetByteArrayAsync(url);
                await File.WriteAllBytesAsync(cacheFilePath, bytes);
                Thread.Sleep(delay);
            }
            return bytes;
        }

        public static async Task<string> GetAsync(this string url, int delay = 0)
        {
            var bytes = await url.GetBytesAsync(delay);
            var str = Encoding.UTF8.GetString(bytes);
            return str;
        }

        public static async Task<T> GetAsync<T>(this string url, int delay = 0)
        {
            var json = await url.GetAsync(delay);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}