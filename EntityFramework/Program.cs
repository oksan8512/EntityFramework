
using EntityFramework;
using EntityFramework.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

Console.InputEncoding = System.Text.Encoding.UTF8;
Console.OutputEncoding = System.Text.Encoding.UTF8;


var conf = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

string connStr = conf["ConnectionStrings:DefaultConnection"];
Console.WriteLine("Connection app {0}", connStr);


var optionsBuilder = new DbContextOptionsBuilder<BookDbContext>();
optionsBuilder.UseNpgsql(connStr);

using (var context = new BookDbContext(optionsBuilder.Options))
{
    context.Database.Migrate(); 
    var authorService = new AuthorServices(context);
    var consoleService = new ConsoleServices(authorService);
    consoleService.Run();
}