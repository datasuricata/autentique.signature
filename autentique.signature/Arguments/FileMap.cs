using System.Text.Json.Serialization;

namespace autentique.signature.Arguments
{
    public class FileMap
    {
        [JsonPropertyName("file")]
        public string[] File { get; set; }

        public static FileMap CreateFileMap() => new() { File = new[] { "variables.file" } };
    }
}
