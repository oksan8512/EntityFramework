using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EntityFramework.Services;

public class AuthorServices
{
    private List<Author> authors;
    private int id;
    private const string filePath = "authors.json";

    public AuthorServices(List<Author> authors, int id)
    {
        this.authors = authors;
        this.id = id;
        LoadAuthors();
    }

    public void AddAuthors(Author author)
    {
        author.Id = this.id++;
        authors.Add(author);

        SafeAuthors();
    }

    public void GetAllAuthors()
    {
        if (!authors.Any())
        {
            Console.WriteLine("Список авторів порожній.");
            return;
        }

        foreach (var author in authors)
        {
            Console.WriteLine(author); 
        }
    }


    public void DeleteAuthors(int id)
    {
        var authorToDelete = GetAuthorById(id);

        if (authorToDelete != null)
        {
            authors.Remove(authorToDelete);
            SafeAuthors();
        }
        else
            Console.WriteLine($"Author with ID {id} not found.");
        
    }

    public Author? GetAuthorById(int id)
    {
        return authors.FirstOrDefault(a => a.Id == id);
    }

    public bool UpdateAuthor(int id, Author updatedAuthor)
    {
        var existingAuthor = GetAuthorById(id);

        if (existingAuthor == null)
            return false;

        updatedAuthor.Id = id;
        var index = authors.IndexOf(existingAuthor);

        authors[index] = updatedAuthor;

        SafeAuthors();
        Console.WriteLine($"Автора '{updatedAuthor.FirstName} {updatedAuthor.LastName}' успішно оновлено!");
        return true;
    }

    public void SafeAuthors()
    {
        var json = System.Text.Json.JsonSerializer.Serialize(authors);
        File.WriteAllText(filePath, json);
    }

    public void LoadAuthors()
    {
        if (!File.Exists(filePath))
        {
            authors = new List<Author>();
            id = 1;
            SafeAuthors(); 
            Console.WriteLine("Файл authors.json не знайдено. Створено новий порожній файл.");
            return;
        }

        var json = File.ReadAllText(filePath);
        authors = JsonSerializer.Deserialize<List<Author>>(json) ?? new List<Author>();

        if (authors.Any())
        {
            id = authors.Max(a => a.Id) + 1;
        }
    }



}
