# autentique.signature
Assinatura avançada com .net 6.0 e graphql utilizando integração com a plataforma brasileira de assinaturas https://autentique.com.br

O projeto possibilita voce informar uma url de um documento (PDF) online, o algoritimo carrega temporariamente o arquivo na pasta do seus sistema operacional para realizar a transmissão do arquivo via graphQL para o serviço avançado de autenticação.
https://github.com/datasuricata/autentique.signature.git

Para rodar o projeto:
- altere a URL do arquivo.pdf
- altere o valor pela sua chave de api gerada no painel do https://autentique.com.br
- altere os dados do destinatario de e-mail
- rode o seguinte comando após as alterações "dotnet run"
