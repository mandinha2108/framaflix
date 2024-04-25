using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FramaFlix.Models;

[Table("Genre")]
    public class Genre
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public byte Id { get; set; }

    [Display(Name = "Nome")]

    public string Name { get; set; }
}
