using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework;

[Table("tblBooks")]
public class Book
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    public int PublicationYear { get; set; }

    [ForeignKey("AuthorEntity")]
    public int AuthorId { get; set; }

    public virtual Author Author { get; set; }

    public override string ToString()
    {
        return $"ID: {Id}, Назва: {Title}, Рік видання: {PublicationYear}, Автор ID: {AuthorId}";
    }
}

