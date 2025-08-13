using EntityFramework.Services;
using EntityFramework;
using System;

namespace EntityFramework;
public class ConsoleServices
{
    private readonly AuthorServices _authorServices;

    public ConsoleServices(AuthorServices authorServices)
    {
        _authorServices = authorServices;
    }

    private void ShowMenu()
    {
        Console.WriteLine("\nГОЛОВНЕ МЕНЮ:");
        Console.WriteLine("1. Показати всіх авторів та їх книги");
        Console.WriteLine("2. Додати нового автора");
        Console.WriteLine("3. Редагувати автора");
        Console.WriteLine("4. Видалити автора");
        Console.WriteLine("5. Додати нову книгу");
        Console.WriteLine("6. Редагувати книгу");
        Console.WriteLine("7. Видалити книгу");
        Console.WriteLine("0. Вихід");
        Console.Write("\nВиберіть опцію: ");
    }

    public void Run()
    {
        while (true)
        {
            ShowMenu();
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    _authorServices.GetAllAuthors();
                    break;
                case "2":
                    AddNewAuthor();
                    break;
                case "3":
                    EditAuthor();
                    break;
                case "4":
                    DeleteAuthor();
                    break;
                case "5":
                    AddNewBook();
                    break;
                case "6":
                    EditBook();
                    break;
                case "7":
                    DeleteBook();
                    break;
                case "0":
                    Console.WriteLine("Вихід з програми.");
                    return;
                default:
                    Console.WriteLine("Невірний вибір, спробуйте ще раз.");
                    break;
            }
        }
    }

    private void AddNewAuthor()
    {
        Console.Write("Ім'я: ");
        var firstName = Console.ReadLine();

        Console.Write("Прізвище: ");
        var lastName = Console.ReadLine();

        Console.Write("Країна: ");
        var country = Console.ReadLine();

        Console.Write("Дата народження (рррр-мм-дд): ");
        if (!DateTime.TryParse(Console.ReadLine(), out var birthDate))
        {
            Console.WriteLine("Невірний формат дати.");
            return;
        }

        Console.Write("Біографія: ");
        var biography = Console.ReadLine();

        var newAuthor = new Author(0, firstName, lastName, country, birthDate, biography);
        _authorServices.AddAuthor(newAuthor);

        Console.WriteLine("Автор успішно доданий.");
    }

    private void EditAuthor()
    {
        Console.Write("Введіть ID автора для редагування: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Невірний ID.");
            return;
        }

        var existingAuthor = _authorServices.GetAuthorById(id);
        if (existingAuthor == null)
        {
            Console.WriteLine("Автор не знайдений.");
            return;
        }

        Console.Write($"Нове ім'я ({existingAuthor.FirstName}): ");
        var firstName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(firstName)) firstName = existingAuthor.FirstName;

        Console.Write($"Нове прізвище ({existingAuthor.LastName}): ");
        var lastName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(lastName)) lastName = existingAuthor.LastName;

        Console.Write($"Нова країна ({existingAuthor.Country}): ");
        var country = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(country)) country = existingAuthor.Country;

        Console.Write($"Нова дата народження ({existingAuthor.BirthDate:yyyy-MM-dd}): ");
        var birthDateStr = Console.ReadLine();
        var birthDate = existingAuthor.BirthDate;
        if (DateTime.TryParse(birthDateStr, out var parsedDate)) birthDate = parsedDate;

        Console.Write("Нова біографія: ");
        var biography = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(biography)) biography = existingAuthor.Biography;

        var updatedAuthor = new Author(id, firstName, lastName, country, birthDate, biography);
        _authorServices.UpdateAuthor(id, updatedAuthor);
    }

    private void DeleteAuthor()
    {
        Console.Write("Введіть ID автора для видалення: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Невірний ID.");
            return;
        }

        _authorServices.DeleteAuthor(id);
    }

    private void AddNewBook()
    {
        Console.Write("Введіть ID автора: ");
        if (!int.TryParse(Console.ReadLine(), out var authorId))
        {
            Console.WriteLine("Невірний ID.");
            return;
        }

        var author = _authorServices.GetAuthorById(authorId);
        if (author == null)
        {
            Console.WriteLine("Автор не знайдений.");
            return;
        }

        Console.Write("Назва книги: ");
        var title = Console.ReadLine();

        Console.Write("Рік видання: ");
        if (!int.TryParse(Console.ReadLine(), out var year))
        {
            Console.WriteLine("Невірний рік.");
            return;
        }

        var newBook = new Book { Title = title, PublicationYear = year, AuthorId = authorId };
        _authorServices.AddBook(newBook);

        Console.WriteLine("Книга успішно додана.");
    }

    private void EditBook()
    {
        Console.Write("Введіть ID книги для редагування: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Невірний ID.");
            return;
        }

        var book = _authorServices.GetBookById(id); 
        if (book == null)
        {
            Console.WriteLine("Книга не знайдена.");
            return;
        }

        Console.Write($"Нова назва ({book.Title}): ");
        var title = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(title)) title = book.Title;

        Console.Write($"Новий рік видання ({book.PublicationYear}): ");
        var yearStr = Console.ReadLine();
        var year = book.PublicationYear;
        if (int.TryParse(yearStr, out var parsedYear)) year = parsedYear;

        Console.Write($"Новий ID автора ({book.AuthorId}): ");
        var authorIdStr = Console.ReadLine();
        var authorId = book.AuthorId;
        if (int.TryParse(authorIdStr, out var parsedAuthorId))
        {
            var author = _authorServices.GetAuthorById(parsedAuthorId);
            if (author == null)
            {
                Console.WriteLine("Автор не знайдений.");
                return;
            }
            authorId = parsedAuthorId;
        }

        var updatedBook = new Book { Id = id, Title = title, PublicationYear = year, AuthorId = authorId };
        _authorServices.UpdateBook(id, updatedBook);
    }

    private void DeleteBook()
    {
        Console.Write("Введіть ID книги для видалення: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Невірний ID.");
            return;
        }

        _authorServices.DeleteBook(id);
    }
}