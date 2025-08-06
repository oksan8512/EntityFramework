using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework;

public class Author
{

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Country { get; set; }
    public DateTime BirthDate { get; set; }
    public string Biography { get; set; }

    public Author(int id, string firstName, string lastName, string country, DateTime birthDate, string biography)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Country = country;
        BirthDate = birthDate;
        Biography = biography;
    }

    public override string ToString()
    {
        var birthDateStr = BirthDate.ToString("dd.MM.yyyy") ?? "Невідомо";

        return $"ID: {Id}\n" +
               $"Ім'я: {FirstName} {LastName}\n" +
               $"Дата народження: {birthDateStr}\n" +
               $"Біографія: {Biography}\n" +
               new string('-', 50);
    }
}

