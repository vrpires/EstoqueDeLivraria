# EstoqueDeLivraria

Este projeto é um exemplo de um sistema de controle de estoque simples de uma livraria.

Foi desenvolvida uma aplicação MVC, utilizando:

* Framework .NET 4.6.1
* Framework de ORM ADO.NET para persistência dos dados
* SQL Server Management 2012.

Além de Razor, Javascript, jQuery e AJAX para o CRUD.
Várias classes de documentos são disponibilizadas, como por exemplo:

### Banco de Dados

Para este sistema foi criado um banco de dados denominado **estoque-de-livros**.

Após a criação do banco, foi criado um novo login com autenticação pelo SQL Server na pasta Security->Login no SQL Server para acesso direto ao banco **estoque-de-livros**.

Em seguida foi incluído um novo usuário na pasta Security do banco de dados **estoque-de-livros** para inclusão deste usuário recém criado.

A rota de configuração de acesso ao banco encontra-se no arquivo **Web.config**
