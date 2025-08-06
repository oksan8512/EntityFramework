using EntityFramework.Services;
using EntityFramework;

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
        Console.WriteLine("1. Показати всіх авторів");
        Console.WriteLine("2. Додати нового автора");
        Console.WriteLine("3. Редагувати автора");
        Console.WriteLine("4. Видалити автора");
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
        _authorServices.AddAuthors(newAuthor);

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

        _authorServices.DeleteAuthors(id);
    }
}
