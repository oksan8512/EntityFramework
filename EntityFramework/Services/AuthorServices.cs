using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFramework.Services;

public class AuthorServices
{
    private readonly BookDbContext _context;

    public AuthorServices(BookDbContext context)
    {
        _context = context;
        SeedInitialData(); 
    }

    private void SeedInitialData()
    {
        if (!_context.Authors.Any())
        {
            var authors = new List<Author>
            {
                new Author(0, "Вільям", "Шекспір", "Англія", new DateTime(1564, 4, 26), "Великий драматург"),
                new Author(0, "Дж. Р. Р.", "Толкін", "Велика Британія", new DateTime(1892, 1, 3), "Автор 'Володаря перснів'"),
                new Author(0, "Дж. К.", "Роулінг", "Велика Британія", new DateTime(1965, 7, 31), "Автор 'Гаррі Поттера'")
            };
            _context.Authors.AddRange(authors);
            _context.SaveChanges();
        }
    }

    public void AddAuthor(Author author)
    {
        _context.Authors.Add(author);
        _context.SaveChanges();
    }

    public void GetAllAuthors()
    {
        var authors = _context.Authors.Include(a => a.Books).ToList();
        if (!authors.Any())
        {
            Console.WriteLine("Список авторів порожній.");
            return;
        }

        foreach (var author in authors)
        {
            Console.WriteLine(author);
            if (author.Books.Any())
            {
                Console.WriteLine("Книги автора:");
                foreach (var book in author.Books)
                {
                    Console.WriteLine($"\t{book}");
                }
            }
        }
    }

    public void DeleteAuthor(int id)
    {
        var author = GetAuthorById(id);
        if (author != null)
        {
            _context.Authors.Remove(author);
            _context.SaveChanges();
            Console.WriteLine("Автор видалений (книги видалені через каскадне видалення).");
        }
        else
        {
            Console.WriteLine($"Автор з ID {id} не знайдений.");
        }
    }

    public Author GetAuthorById(int id)
    {
        return _context.Authors.FirstOrDefault(a => a.Id == id);
    }

    public bool UpdateAuthor(int id, Author updatedAuthor)
    {
        var existingAuthor = GetAuthorById(id);
        if (existingAuthor == null)
            return false;

        existingAuthor.FirstName = updatedAuthor.FirstName;
        existingAuthor.LastName = updatedAuthor.LastName;
        existingAuthor.Country = updatedAuthor.Country;
        existingAuthor.BirthDate = updatedAuthor.BirthDate;
        existingAuthor.Biography = updatedAuthor.Biography;

        _context.SaveChanges();
        Console.WriteLine($"Автора '{updatedAuthor.FirstName} {updatedAuthor.LastName}' успішно оновлено!");
        return true;
    }

    public void AddBook(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public bool UpdateBook(int id, Book updatedBook)
    {
        var existingBook = _context.Books.FirstOrDefault(b => b.Id == id);
        if (existingBook == null)
            return false;

        existingBook.Title = updatedBook.Title;
        existingBook.PublicationYear = updatedBook.PublicationYear;
        existingBook.AuthorId = updatedBook.AuthorId;

        _context.SaveChanges();
        Console.WriteLine($"Книгу '{updatedBook.Title}' успішно оновлено!");
        return true;
    }

    public void DeleteBook(int id)
    {
        var book = _context.Books.FirstOrDefault(b => b.Id == id);
        if (book != null)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
            Console.WriteLine("Книга видалена.");
        }
        else
        {
            Console.WriteLine($"Книга з ID {id} не знайдена.");
        }
    }


    public Book GetBookById(int id)
    {
        return _context.Books.FirstOrDefault(b => b.Id == id);
    }
}