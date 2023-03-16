using autentique.signature.Arguments;
using autentique.signature.Extensions;
using autentique.signature.Mutations;
using RestSharp;

namespace autentique.signature
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var fileInfo = await UtilsExtensions.ToTempFileFromUriAsync(new Uri("https://troque-pela-url-do-seu-documento.pdf"));
            var fileMap = FileMap.CreateFileMap();
            var mutation = CreateDocumentMutation.CreateDocument(
                variables: new
                {
                    document = new
                    {
                        name = "Nome do documento",
                        message = "Mensagem customizada enviada para os emails dos signatários",
                        show_audit_page = false
                    },
                    signers = new[] { new { email = "troque-pelo-email-publico@provedor.com.br", action = "SIGN", configs = new { cpf = "0000000000" } } }
                });

            var client = new RestClient("https://api.autentique.com.br/");
            var request = new RestRequest("v2/graphql", Method.Post)
                .AddHeader("Authorization", "Bearer YOUR_API_KEY")
                .AddParameter("operations", mutation.Serialize())
                .AddParameter("map", fileMap.Serialize())
                .AddFile("file", fileInfo.FullName);

            var response = await client.ExecuteAsync(request);

            Console.WriteLine(response.Content);
            UtilsExtensions.DeleteTempFile(fileInfo);

            Console.ReadLine();
        }
    }
}