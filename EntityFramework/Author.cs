using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework;

[Table("tblAuthors")]
public class Author
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [StringLength(50)]
    public string Country { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; }
    [StringLength(1000)]
    public string Biography { get; set; } = string.Empty;
    public virtual List<Book> Books { get; set; } = new();


    public Author() { } 

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
               $"Країна: {Country}\n" +
               $"Дата народження: {birthDateStr}\n" +
               $"Біографія: {Biography}\n" +
               new string('-', 50);
    }
}