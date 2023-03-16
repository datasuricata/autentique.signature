using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace autentique.signature.Extensions
{
    public static class UtilsExtensions
    {
        public static string Serialize(this object value)
        {
            var options = new JsonSerializerOptions { WriteIndented = false, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
            var json = JsonSerializer.Serialize(value, options);
            var clean = json.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries)
                .ToList().Select(x => x.Trim());
            return Regex.Unescape(string.Join(" ", clean));
        }

        public static string Compress(this string value)
        {
            //windows CRLF
            if (IsWindows())
                return value.Replace("\r\n", string.Empty).Replace("\t", string.Empty);

            //LF or POSIX
            return value.Replace("\n", string.Empty).Replace("\t", string.Empty);

            static bool IsWindows() => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        public static async Task<FileInfo> ToTempFileFromUriAsync(Uri url)
        {
            string tempfile = Path.GetTempFileName();
            File.Delete(tempfile);

            var filename = url.PathAndQuery.Split('/').LastOrDefault();
            if (filename == null)
                throw new FileNotFoundException(filename);

            var path = Path.Combine(tempfile, filename);
            Directory.CreateDirectory(tempfile);

            var client = new HttpClient();
            using (var fs = new FileStream(path, FileMode.CreateNew))
                await (await client.GetAsync(url)).Content.CopyToAsync(fs);

            return new FileInfo(path);
        }

        public static void DeleteTempFile(FileInfo info)
        {
            if (info is null || info.DirectoryName is null)
                return;

            Directory.Delete(info.DirectoryName,
                recursive: true);
        }
    }
}