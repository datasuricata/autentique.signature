using autentique.signature.Extensions;
using System.Text.Json.Serialization;

namespace autentique.signature.Arguments
{
    public class MutationRequest
    {
        [JsonPropertyName("query")]
        public string Query { get; set; }

        [JsonPropertyName("variables")]
        public object Variables { get; set; }

        public static MutationRequest CreateMutation(string mutation, object variables) 
            => new() { Variables = variables, Query = mutation.Compress() };
    }
}
