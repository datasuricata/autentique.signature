using autentique.signature.Arguments;

namespace autentique.signature.Mutations
{
    public static class CreateDocumentMutation
    {
        public static MutationRequest CreateDocument(object variables)
        {
            return MutationRequest.CreateMutation(
                mutation: @"
                mutation CreateDocumentMutation($document: DocumentInput!, $signers: [SignerInput!]!, $file: Upload!)
                {
                  createDocument(document: $document, signers: $signers, file: $file) 
                  {
                    id
                    name
                    created_at
                  }
                }",
                variables: variables);
        }
    }
}
