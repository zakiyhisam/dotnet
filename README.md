# dotnet rest api application
Good reference to setup a http response status code:
https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status
https://www.iana.org/assignments/http-status-codes/http-status-codes.xhtml
https://en.wikipedia.org/wiki/List_of_HTTP_status_codes

General rules:
1xx (Informational): The server received the request header, please wait.
2xx (Success): The action was successfully processed and accepted.
3xx (Redirection): The client must take extra steps at a different URL.
4xx (Client Error): The client made a mistake (bad data, wrong URL, missing login).
5xx (Server Error): The client did everything right, but your internal C# code crashed.

Bash command:

//Install EF Core library and database driver

dotnet add package Microsoft.EntityFrameworkCore.InMemory //.Sqlite or .SqlServer for local computer

//Example for PostgreSQL
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

//For connection to real database
dotnet add package Microsoft.EntityFrameworkCore.Design

