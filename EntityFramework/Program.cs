using EntityFramework;
using EntityFramework.Services;
using EntityFramework;
using EntityFramework.Services;

Console.InputEncoding = System.Text.Encoding.UTF8;
Console.OutputEncoding = System.Text.Encoding.UTF8;

var authors = new List<Author>();
var authorService = new AuthorServices(authors, 1);
var consoleService = new ConsoleServices(authorService);

consoleService.Run();
